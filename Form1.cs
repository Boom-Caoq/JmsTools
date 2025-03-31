using NBitcoin;
using Nethereum.HdWallet;
using Nethereum.Util;
using Nethereum.Signer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using Solana.Unity.Wallet;
using Solana.Unity.Rpc;
using System.Net.Sockets;
using NBitcoin.RPC;
using Solana.Unity.Rpc.Messages;
using Nethereum.Web3;
using System.Numerics;
using System.Globalization;
using jmsTools.Manage;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Text.RegularExpressions;
using static NBitcoin.RPC.SignRawTransactionRequest;
using System.Diagnostics;
using Nethereum.Contracts.QueryHandlers.MultiCall;
using Newtonsoft.Json.Linq;
using Timer = System.Windows.Forms.Timer;

namespace jmsTools
{
    public partial class Form1 : Form
    {

        // 存储从文件读取的RPC节点列表
        private readonly string[] EthrpcNodes;

        // 存储从文件读取的RPC节点列表
        private readonly string[] BscrpcNodes;
        // 助记词单词表
        private readonly string[] wordList;
        // 已经检查过的地址集合
        private readonly HashSet<string> seenAddresses = new HashSet<string>();
        // 程序启动时间
        private DateTime startTime;
        // 查询计数
        private int queryCount = 0;

        private bool _isRunning = false; // 控制是否继续执行任务

        private readonly SemaphoreSlim _throttle = new SemaphoreSlim(5); // 控制并发度为10

        private string selectedNetwork = "ETH"; // 默认选中的网络

        private readonly object _lockObject = new object();//线程锁

        private int threadCount = 5;//线程数量

        private string prefix = "";
        private string suffix = "";

        // 标志变量，用于指示是否已经找到匹配
        private volatile bool foundMatch = false;


        //示例（靓号生成功能）ETH地址
        private string originalEthAddress = "0x816e4a1589e363720c15c54dfd2efd16f6377070"; // 原始ETH地址  用于靓号生成功能演示
        private string currentEthAddress; // 当前ETH地址

        private int remainingSeconds;

        private string predictedTrend; // 综合趋势方向（涨或跌）

        private double initialPrice; // 用于存储初始价格

        public Form1()
        {
            InitializeComponent();

            // 加载RPC节点列表和助记词单词表
            EthrpcNodes = File.ReadAllLines("Ethrpc_nodes.txt");
            BscrpcNodes = File.ReadAllLines("Bscrpc_nodes.txt");
            wordList = File.ReadAllLines("bip39.txt");

            // 初始化currentEthAddress为originalEthAddress
            currentEthAddress = originalEthAddress;
            UpdateRichTextBox();

            InitializeCountdownTimer();

        }




        private void UpdateRichTextBox()
        {
            xample_str.Text = currentEthAddress;
            xample_str.SelectAll();
            xample_str.SelectionColor = Color.Black; // 恢复默认颜色

            string prefix = textBoxPrefix.Text;
            int prefixLength = Math.Min(prefix.Length, 6);
            if (prefixLength > 0)
            {
                xample_str.SelectionStart = 2;
                xample_str.SelectionLength = prefixLength;
                xample_str.SelectionColor = Color.Red; // 高亮颜色
            }

            string suffix = textBoxSuffix.Text;
            int suffixLength = suffix.Length;
            if (suffixLength > 0)
            {
                xample_str.SelectionStart = currentEthAddress.Length - suffixLength;
                xample_str.SelectionLength = suffixLength;
                xample_str.SelectionColor = Color.Red; // 高亮颜色
            }
        }


        /// <summary>
        /// 开始执行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ethbtn_start_Click(object sender, EventArgs e)
        {
            startTime = DateTime.Now;
            _isRunning = true;

            LogMessage("\n 开始执行" + selectedNetwork + "主网碰撞.初始化中，请稍等..\n");
            StartProcess(); // 启动处理助记词的进程
        }

        // 开始处理助记词
        private void StartProcess()
        {
            Task.Run(() => ProcessMnemonic()); // 在新任务中运行助记词处理逻辑
        }

        // 处理助记词生成、地址生成和交易查询逻辑
        private async Task ProcessMnemonic()
        {
            while (_isRunning) // 检查_isRunning标志
            {
                await _throttle.WaitAsync(); // 等待信号量

                _ = Task.Run(async () =>
                {
                    try
                    {
                        if (!_isRunning) return; // 再次检查_isRunning标志

                        // 根据主网类型生成钱包
                        (string address, string privateKey) walletInfo;

                        switch (selectedNetwork.ToLower())
                        {
                            case "eth":
                            case "bsc":
                                var ethWallet = new Nethereum.HdWallet.Wallet(Wordlist.English, WordCount.Twelve);
                                var ethAccount = ethWallet.GetAccount(0);
                                walletInfo = (
                                    new AddressUtil().ConvertToChecksumAddress(ethAccount.Address),
                                    ethAccount.PrivateKey
                                );
                                break;
                            case "sol":
                                var solWallet = new Solana.Unity.Wallet.Wallet(Solana.Unity.Wallet.Bip39.WordCount.Twelve, Solana.Unity.Wallet.Bip39.WordList.English, "", SeedMode.Bip39);
                                walletInfo = (
                                    solWallet.Account.PublicKey.Key,
                                    solWallet.Account.PrivateKey // Solana私钥通常表示为Base58编码的字符串
                                );
                                break;
                            default:
                                throw new NotSupportedException("不支持的主网类型");
                        }

                        var address = walletInfo.address;
                        var privateKey = walletInfo.privateKey;

                        string[] rpcNodes = new string[1];
                        if (!seenAddresses.Contains(address)) // 如果该地址未被检查过
                        {
                            lock (_lockObject) // 确保线程安全地添加到集合中
                            {
                                seenAddresses.Add(address);
                            }


                            var transactionCount = await BlockManage.GetBalanceInfoByPz(address, selectedNetwork); // 查询余额，比查询交易次数更快，同样针对主网原生代币查询
                            if (transactionCount.HasValue)
                            {
                                // 在日志中包含私钥信息
                                LogMessage($"\n地址: {address} 私钥: {privateKey} 余额:{transactionCount}\n");
                                if (transactionCount > 0)
                                {
                                    // 发送通知逻辑可以在这里实现
                                }
                            }
                            else
                            {
                                // 即使没有交易，也选择记录地址和私钥
                                LogMessage($"\n地址: {address} 私钥: {privateKey} 余额:{0}\n");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // 记录异常信息以便调试
                        //LogMessage($"\n发生错误: {ex.Message}\n");
                    }
                    finally
                    {
                        _throttle.Release(); // 释放信号量
                    }
                });

                if (!_isRunning) break; // 如果停止了，则跳出循环
            }
        }


        /// <summary>
        /// 日志记录-碰撞器
        /// </summary>
        /// <param name="message"></param>

        private void LogMessage(string message)
        {
            if (this.EthtextBoxLog.InvokeRequired)
            {
                EthtextBoxLog.Invoke(new Action<string>(LogMessage), new object[] { message });
            }
            else
            {
                EthtextBoxLog.AppendText(Environment.NewLine + $"\n{message}\n");
                EthlabelRunTime.Text = queryCount.ToString() + "次";
                queryCount++;
            }
        }


        /// <summary>
        /// 日志记录-交易记录
        /// </summary>
        /// <param name="message"></param>

        private void LogMessageTransaction(string message)
        {
            if (this.text_transaction.InvokeRequired)
            {
                text_transaction.Invoke(new Action<string>(LogMessageTransaction), new object[] { message });
            }
            else
            {
                text_transaction.AppendText(Environment.NewLine + $"\n{message}\n");
            }
        }


        /// <summary>
        /// 停止
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ethbtn_stop_Click(object sender, EventArgs e)
        {
            _isRunning = false;
            LogMessage("\n停止处理...\n");
        }


        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {

            com_Currency.SelectedIndex = 0; // 默认选中第一个项
            com_Cycle.SelectedIndex = 0; // 默认选中第一个项

            comboBoxMainNet.SelectedIndex = 0; // 默认选中第一个项
            selectedNetwork = comboBoxMainNet.SelectedItem.ToString();
        }

        /// <summary>
        /// 主网切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxMainNet_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxMainNet.SelectedItem != null)
            {
                selectedNetwork = comboBoxMainNet.SelectedItem.ToString();
                LogMessage("\n 已切换至" + selectedNetwork + "主网...\n");
            }
        }


        /// <summary>
        /// 单地址查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void oneaddress_cx_Click(object sender, EventArgs e)
        {
            // 判断碰撞是否正在运行
            if (_isRunning)
            {
                MessageBox.Show("请先停止碰撞进程再进行单地址查询！", "操作冲突", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(this.oneaddress.Text))
            {
                MessageBox.Show("请输入查询地址！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            await SomeMethod(oneaddress.Text, selectedNetwork);
        }


        public static long GetTimeStamp(bool accurateToMilliseconds = false)
        {
            if (accurateToMilliseconds)
            {
                return DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            }
            else
            {
                return DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            }
        }

        /// <summary>
        /// 单地址余额查询
        /// </summary>
        /// <param name="address"></param>
        /// <param name="network"></param>
        /// <returns></returns>
        public async Task SomeMethod(string address, string network)
        {




            long sp = GetTimeStamp(false);
            string url = "";
            switch (network.ToLower())
            {
                case "eth":
                    url = $"https://eth.tokenview.io/api/search/{address}";
                    break;
                case "bsc":
                    url = $"https://bsc.tokenview.io/api/bsc/address/{address}";
                    break;
                default:
                    break;
            }
            var a = await ExampleRequest.ExecuteGetRequestAsync(url);

            switch (network.ToLower())
            {
                case "eth":
                    if (!string.IsNullOrEmpty(a))
                    {
                        var apiResponse = JsonConvert.DeserializeObject<EthApiResponseModel>(a);

                        if (apiResponse.EnMsg == "SUCCESS")
                        {


                            var str = "";
                            int x_ = 80;
                            foreach (var item in apiResponse.Data)
                            {
                                decimal Balance = Convert.ToDecimal(item.Balance);
                                Balance = Math.Round(Balance, 4);

                                str = str + Balance + "" + item.Network + "  ";
                                //this.Controls.Add(newLabel);
                            }

                            BalanceInfo.Text = str;
                        }

                    }
                    break;
                case "bsc":
                    break;
                default:
                    break;
            }
        }


        /// <summary>
        /// 前缀
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxPrefix_TextChanged(object sender, EventArgs e)
        {
            // 使用正则表达式匹配允许的字符（数字和A到F的大写或小写字母）
            Regex regex = new Regex("^[0-9a-fA-F]*$");
            // 检查文本是否与模式匹配
            if (!regex.IsMatch(textBoxPrefix.Text))
            {
                // 提示用户只允许输入特定字符
                MessageBox.Show("请输入数值(0-9)或A-F之间的字母", "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }
            prefix = textBoxPrefix.Text;
            HandleReplacement();
            TextChanged();
        }


        private bool IsAlphaNumeric(string input)
        {
            foreach (char c in input)
            {
                if (!char.IsLetterOrDigit(c))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// 后缀
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxSuffix_TextChanged(object sender, EventArgs e)
        {
            // 使用正则表达式匹配允许的字符（数字和A到F的大写或小写字母）
            Regex regex = new Regex("^[0-9a-fA-F]*$");
            // 检查文本是否与模式匹配
            if (!regex.IsMatch(textBoxSuffix.Text))
            {
                // 提示用户只允许输入特定字符
                MessageBox.Show("请输入数值(0-9)或A-F之间的字母", "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }
            suffix = textBoxPrefix.Text;
            HandleReplacement();
            TextChanged();
        }

        /// <summary>
        /// 前后缀输入处理
        /// </summary>
        private void HandleReplacement()
        {
            string prefix = textBoxPrefix.Text;
            string suffix = textBoxSuffix.Text;

            // 验证前缀和后缀是否为字母或数字





            // 从原始地址开始构建新的ETH地址
            string newEthAddress = originalEthAddress;

            // 前缀替换
            int prefixLength = Math.Min(prefix.Length, 6); // 控制最多替换6位
            if (prefixLength > 0)
            {
                newEthAddress = "0x" + prefix.Substring(0, prefixLength) + originalEthAddress.Substring(2 + prefixLength);
            }

            // 后缀替换
            int suffixLength = suffix.Length;
            if (suffixLength > 0 && newEthAddress.Length >= suffixLength)
            {
                newEthAddress = newEthAddress.Substring(0, newEthAddress.Length - suffixLength) + suffix;
            }
            else if (suffixLength > 0)
            {
                MessageBox.Show("后缀长度超过了ETH地址的长度");
                return;
            }

            // 更新当前ETH地址并刷新RichTextBox显示
            currentEthAddress = newEthAddress;
            UpdateRichTextBox();
        }


        /// <summary>
        /// 详情动态
        /// </summary>
        private void TextChanged()
        {
            string prefix = textBoxPrefix.Text;
            string suffix = textBoxSuffix.Text;

            var difficulty = EthAddressDifficultyCalculator.CalculateDifficulty(prefix, suffix);
            complexity_lab.Text = difficulty.complexity.ToString();
            ygtime.Text = difficulty.estimatedTime;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // 打开默认浏览器并导航到指定网址
            Process.Start(new ProcessStartInfo("https://bqbot.cn") { UseShellExecute = true });
        }


        // 线程安全的计数器
        private long totalGeneratedCount = 0;
        private CancellationTokenSource cancellationTokenSource;

        /// <summary>
        /// 地址生成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void generate_btn_Click(object sender, EventArgs e)
        {
            labelResults.Text = "";
            // 清空之前的记录
            labelResults.AppendText("正在生成中，请稍候...");
            // 重置计数器
            Interlocked.Exchange(ref totalGeneratedCount, 0);
            foundMatch = false;
            // 更新界面上的计数器
            UpdateGeneratedCountLabel();

            // 初始化取消令牌
            cancellationTokenSource = new CancellationTokenSource();

            // 启动生成任务
            Task.Run(() => GenerateWallets(cancellationTokenSource.Token));
        }



        private void GenerateWallets(CancellationToken cancellationToken)
        {
            var tasks = new Task[threadCount];

            for (int i = 0; i < threadCount; i++)
            {
                tasks[i] = Task.Run(() =>
                {
                    while (!cancellationToken.IsCancellationRequested)
                    {
                        // 生成钱包地址
                        var ethWallet = new Nethereum.HdWallet.Wallet(Wordlist.English, WordCount.Twelve);
                        var ethAccount = ethWallet.GetAccount(0);
                        var address = ethAccount.Address; // 转为小写

                        // 增加生成计数
                        Interlocked.Increment(ref totalGeneratedCount);

                        // 更新界面上的计数器
                        UpdateGeneratedCountLabel();

                        // 检查是否符合前后缀要求
                        if (IsAddressValid(address))
                        {
                            // 设置标志变量为 true
                            foundMatch = true;
                            var privateKey = ethAccount.PrivateKey;
                            // 更新界面上的结果
                            UpdateResultsLabel($"地址：{address}");
                            UpdateResultsLabel($"私钥：{privateKey}");
                            // 取消所有线程的任务
                            cancellationTokenSource.Cancel();
                        }
                    }
                }, cancellationToken);
            }

            try
            {
                Task.WaitAll(tasks, cancellationToken);
            }
            catch (OperationCanceledException)
            {
                // 用户停止生成任务
            }
        }


        private void UpdateResultsLabel(string result)
        {
            // 确保在UI线程中更新控件
            if (labelResults.InvokeRequired)
            {
                labelResults.Invoke(new Action<string>(UpdateResultsLabel), result);
            }
            else
            {
                // 将新结果追加到现有内容中，并添加换行符
                labelResults.AppendText(Environment.NewLine + result + Environment.NewLine);
            }
        }


        private bool IsAddressValid(string address)
        {

            // 移除地址中的 "0x" 前缀
            string addressWithoutPrefix = address.StartsWith("0x", StringComparison.OrdinalIgnoreCase) ? address.Substring(2) : address;

            // 根据用户选择决定是否区分大小写
            if (!checkBox1.Checked) // 如果未勾选，则不区分大小写
            {
                addressWithoutPrefix = addressWithoutPrefix.ToLower();
                prefix = prefix.ToLower();
                suffix = suffix.ToLower();
            }

            // 检查前缀和后缀
            bool isPrefixMatch = string.IsNullOrEmpty(prefix) || addressWithoutPrefix.StartsWith(prefix);
            bool isSuffixMatch = string.IsNullOrEmpty(suffix) || addressWithoutPrefix.EndsWith(suffix);

            return isPrefixMatch && isSuffixMatch;
        }


        private void UpdateGeneratedCountLabel()
        {
            // 确保在UI线程中更新控件
            if (generated_lab.InvokeRequired)
            {
                generated_lab.Invoke(new Action(UpdateGeneratedCountLabel));
            }
            else
            {
                generated_lab.Text = $"{totalGeneratedCount}";
            }
        }

        private void taskNum_ValueChanged(object sender, EventArgs e)
        {
            threadCount = (int)this.taskNum.Value;
        }

        private void stop_btn_Click(object sender, EventArgs e)
        {
            // 如果任务正在进行，则取消所有线程的任务
            if (cancellationTokenSource != null)
            {
                cancellationTokenSource.Cancel();
                labelResults.AppendText("任务已手动停止" + Environment.NewLine);
            }
        }

        /// <summary>
        /// 更新倒计时 Label
        /// </summary>
        private void UpdateCountdownLabel(int remainingSeconds)
        {
            int minutes = remainingSeconds / 60;
            int seconds = remainingSeconds % 60;
            lbl_Countdown.Text = $"倒计时: {minutes} 分 {seconds} 秒";
        }

        /// <summary>
        /// 事件合约分析
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btn_analysis_Click(object sender, EventArgs e)
        {
            // 获取用户选择的币种和周期
            string selectedCurrency = com_Currency.SelectedItem.ToString(); // 如 "BTC/USDT"
            string selectedCycle = com_Cycle.SelectedItem.ToString();       // 如 "10分钟"

            // 转换币种格式
            string symbol = selectedCurrency.Replace("/", ""); // 将 "BTC/USDT" 转换为 "BTCUSDT"

            // 转换周期格式
            string interval = CycleToInterval(selectedCycle); // 将 "10分钟" 转换为 "15m"

            analysis_Log.Text = "";


            // 转换周期格式（如 "10分钟" 转换为秒数）
            int cycleInSeconds = CycleToSeconds(selectedCycle);

            // 启动倒计时
            timer1.Start();

            // 日志输出开始分析的消息
            analysis_Log.Text += $"开始分析 {selectedCurrency} ({selectedCycle})...\n";
            analysis_Log.Text += Environment.NewLine;

            // 记录分析开始时间
            DateTime analysisStartTime = DateTime.Now;
            analysis_Log.Text += $"分析开始时间: {analysisStartTime}\n";
            analysis_Log.Text += Environment.NewLine;

            // 设置倒计时的初始秒数
            timer1.Tag = cycleInSeconds; // ★★★ 初始化 timer1.Tag ★★★


            // 调用API获取历史数据，并进行分析
            await AnalyzeSymbol(symbol, interval);
        }



        /// <summary>
        /// 将周期字符串转换为秒数
        /// </summary>
        private int CycleToSeconds(string cycle)
        {
            switch (cycle)
            {
                case "10分钟":
                    return 10 * 60; // 10 分钟 = 600 秒
                case "30分钟":
                    return 30 * 60; // 30 分钟 = 1800 秒
                case "1小时":
                    return 60 * 60; // 1 小时 = 3600 秒
                case "4小时":
                    return 4 * 60 * 60; // 4 小时 = 14400 秒
                default:
                    throw new ArgumentException("无效的周期格式");
            }
        }

        // 周期转换函数
        private string CycleToInterval(string cycle)
        {
            switch (cycle)
            {
                case "10分钟":
                    return "15m"; // 币安不支持10分钟，用15分钟替代
                case "30分钟":
                    return "30m";
                case "1小时":
                    return "1h";
                case "4小时":
                    return "4h";
                default:
                    throw new ArgumentException("不支持的时间周期");
            }
        }


        /// <summary>
        /// 分析指定币种和周期的历史K线数据，并输出分析结果（含回测）。
        /// </summary>
        /// <param name="symbol">币种标识符，如 "BTCUSDT"。</param>
        /// <param name="interval">K线的时间周期，如 "15m", "30m", "1h", "4h" 等。</param>
        private async Task AnalyzeSymbol(string symbol, string interval)
        {
            try
            {
                // 获取历史K线数据，限制为最近500个周期的数据
                JArray klines = await GetHistoricalData(symbol, interval);

                // 检查是否成功获取到数据
                if (klines == null || klines.Count == 0)
                {
                    analysis_Log.Text += "未获取到有效数据。\n";
                    return;
                }

                // 提取关键数据
                var closingPrices = klines.Select(k => (double)k[4]).ToList(); // 收盘价
                var highPrices = klines.Select(k => (double)k[2]).ToList();    // 最高价
                var lowPrices = klines.Select(k => (double)k[3]).ToList();     // 最低价
                var volumes = klines.Select(k => (double)k[5]).ToList();       // 成交量
                double currentPrice = closingPrices.Last();                   // 当前价格（最新收盘价）

                // 计算技术指标
                double rsi = CalculateRSI(closingPrices, 14);
                var ma = CalculateMA(closingPrices, 20);
                var bollingerBands = CalculateBollingerBands(closingPrices);
                var macdResult = CalculateMACD(closingPrices);
                var stochastic = CalculateStochasticOscillator(highPrices, lowPrices, closingPrices);
                var atr = CalculateATR(highPrices, lowPrices, closingPrices);
                double avgVolume = CalculateAverageVolume(klines, 20);

                // 斐波那契回撤需要高点和低点
                double highestHigh = highPrices.Max();
                double lowestLow = lowPrices.Min();
                var fibonacciRetracement = CalculateFibonacciRetracement(highestHigh, lowestLow);

                // 综合分析
                string trend = AnalyzeTrend(
                    currentPrice,
                    rsi,
                    ma,
                    bollingerBands,
                    macdResult,
                    stochastic,
                    atr,
                    avgVolume,
                    fibonacciRetracement
                );


                // 设置综合趋势方向
                predictedTrend = trend; // 在这里保存趋势方向
                                    
                initialPrice = currentPrice; // 获取当前价格并记录为初始价格
                // 执行回测
                var backtestResult = Backtest(klines);

                // 输出分析结果
                analysis_Log.Text += Environment.NewLine;
                analysis_Log.Text += $"当前价格: {currentPrice:F2}\n";
                analysis_Log.Text += Environment.NewLine;
                analysis_Log.Text += $"RSI值: {rsi:F2}\n";
                analysis_Log.Text += Environment.NewLine;
                analysis_Log.Text += $"移动平均线 (MA): {ma:F2}\n";
                analysis_Log.Text += Environment.NewLine;
                analysis_Log.Text += $"布林带状态: 上轨={bollingerBands.UpperBand:F2}, 下轨={bollingerBands.LowerBand:F2}, 中轨={bollingerBands.SMA:F2}\n";
                analysis_Log.Text += Environment.NewLine;
                analysis_Log.Text += $"MACD信号: {(macdResult.MACDLine > macdResult.SignalLine ? "金叉（看涨）" : "死叉（看跌）")}\n";
                analysis_Log.Text += Environment.NewLine;
                analysis_Log.Text += $"随机指标 (%K): {stochastic.K:F2}, (%D): {stochastic.D:F2}\n";
                analysis_Log.Text += Environment.NewLine;
                analysis_Log.Text += $"平均真实波幅 (ATR): {atr:F2}\n";
                analysis_Log.Text += Environment.NewLine;
                analysis_Log.Text += $"平均成交量: {avgVolume:F2}\n";
                analysis_Log.Text += Environment.NewLine;
                analysis_Log.Text += Environment.NewLine;
                analysis_Log.Text += $"斐波那契回撤: 0%={fibonacciRetracement["0%"]:F2}, 23.6%={fibonacciRetracement["23.6%"]:F2}, 38.2%={fibonacciRetracement["38.2%"]:F2}, 50%={fibonacciRetracement["50%"]:F2}, 61.8%={fibonacciRetracement["61.8%"]:F2}, 100%={fibonacciRetracement["100%"]:F2}\n";
                analysis_Log.Text += Environment.NewLine;
                analysis_Log.Text += Environment.NewLine;
                analysis_Log.Text += $"综合趋势: {trend}\n";
                analysis_Log.Text += Environment.NewLine;
                analysis_Log.Text += $"回测胜率: {backtestResult.WinRate:P2} ({backtestResult.CorrectPredictions}/{backtestResult.TotalPredictions})\n";
                analysis_Log.Text += Environment.NewLine;
            }
            catch (Exception ex)
            {
                analysis_Log.Text += $"发生错误: {ex.Message}\n";
            }
        }



        /// <summary>
        /// 计算平均真实波幅 (ATR)。
        /// </summary>
        /// <param name="highPrices">最高价列表。</param>
        /// <param name="lowPrices">最低价列表。</param>
        /// <param name="closingPrices">收盘价列表。</param>
        /// <param name="period">计算周期，默认为14。</param>
        /// <returns>返回ATR值。</returns>
        private double CalculateATR(IList<double> highPrices, IList<double> lowPrices, IList<double> closingPrices, int period = 14)
        {
            if (highPrices == null || lowPrices == null || closingPrices == null)
                throw new ArgumentException("价格列表不能为空");

            if (highPrices.Count != lowPrices.Count || highPrices.Count != closingPrices.Count)
                throw new ArgumentException("价格列表长度必须一致");

            // 使用索引操作获取最近N个周期的价格数据
            int startIndex = Math.Max(0, highPrices.Count - period);
            var lastHighs = highPrices.Skip(startIndex).Take(period).ToList();
            var lastLows = lowPrices.Skip(startIndex).Take(period).ToList();
            var lastCloses = closingPrices.Skip(startIndex).Take(period).ToList();

            // 计算真实波幅 (TR)
            var trueRanges = new List<double>();
            for (int i = 1; i < lastHighs.Count; i++)
            {
                double tr1 = lastHighs[i] - lastLows[i]; // 当前周期的最高价 - 最低价
                double tr2 = Math.Abs(lastHighs[i] - lastCloses[i - 1]); // 当前周期的最高价 - 前一周期的收盘价
                double tr3 = Math.Abs(lastLows[i] - lastCloses[i - 1]);   // 当前周期的最低价 - 前一周期的收盘价
                trueRanges.Add(Math.Max(tr1, Math.Max(tr2, tr3)));
            }

            // 如果没有足够的数据，直接返回0
            if (trueRanges.Count == 0)
                return 0;

            // 返回真实波幅的平均值作为ATR
            return trueRanges.Average();
        }


        /// <summary>
        /// 计算平均成交量。
        /// </summary>
        /// <param name="klines">K线数据。</param>
        /// <param name="period">计算周期，默认为20。</param>
        /// <returns>返回平均成交量。</returns>
        private double CalculateAverageVolume(JArray klines, int period = 20)
        {
            if (klines == null || klines.Count == 0)
                throw new ArgumentException("K线数据不能为空");

            // 提取成交量数据
            var volumes = klines.Select(k => (double)k[5]).ToList();

            // 使用索引操作获取最近N个周期的成交量数据
            int startIndex = Math.Max(0, volumes.Count - period);
            var lastVolumes = volumes.Skip(startIndex).Take(period).ToList();

            // 返回平均成交量
            return lastVolumes.Average();
        }


        /// <summary>
        /// 计算斐波那契回撤水平。
        /// </summary>
        /// <param name="highestHigh">最高高点。</param>
        /// <param name="lowestLow">最低低点。</param>
        /// <returns>返回斐波那契回撤水平的字典。</returns>
        private Dictionary<string, double> CalculateFibonacciRetracement(double highestHigh, double lowestLow)
        {
            if (highestHigh <= lowestLow)
                throw new ArgumentException("最高高点必须大于最低低点");

            // 定义斐波那契回撤比例
            var fibonacciLevels = new Dictionary<string, double>
    {
        { "0%", highestHigh },
        { "23.6%", highestHigh - (highestHigh - lowestLow) * 0.236 },
        { "38.2%", highestHigh - (highestHigh - lowestLow) * 0.382 },
        { "50%", highestHigh - (highestHigh - lowestLow) * 0.5 },
        { "61.8%", highestHigh - (highestHigh - lowestLow) * 0.618 },
        { "100%", lowestLow }
    };

            return fibonacciLevels;
        }

        /// <summary>
        /// 获取最高高点和最低低点。
        /// </summary>
        /// <param name="highPrices">最高价列表。</param>
        /// <param name="lowPrices">最低价列表。</param>
        /// <param name="period">计算周期，默认为20。</param>
        /// <returns>返回最高高点和最低低点。</returns>
        private (double HighestHigh, double LowestLow) GetHighestAndLowest(IList<double> highPrices, IList<double> lowPrices, int period = 20)
        {
            if (highPrices == null || lowPrices == null)
                throw new ArgumentException("价格列表不能为空");

            if (highPrices.Count != lowPrices.Count)
                throw new ArgumentException("价格列表长度必须一致");

            // 使用索引操作获取最近N个周期的价格数据
            int startIndex = Math.Max(0, highPrices.Count - period);
            var lastHighs = highPrices.Skip(startIndex).Take(period).ToList();
            var lastLows = lowPrices.Skip(startIndex).Take(period).ToList();

            // 计算最高高点和最低低点
            double highestHigh = lastHighs.Max();
            double lowestLow = lastLows.Min();

            return (highestHigh, lowestLow);
        }

        /// <summary>
        /// 计算随机指标 (%K 和 %D)。
        /// </summary>
        /// <param name="highPrices">最高价列表。</param>
        /// <param name="lowPrices">最低价列表。</param>
        /// <param name="closingPrices">收盘价列表。</param>
        /// <param name="period">计算周期，默认为14。</param>
        /// <returns>返回%K和%D值。</returns>
        private (double K, double D) CalculateStochasticOscillator(IList<double> highPrices, IList<double> lowPrices, IList<double> closingPrices, int period = 14)
        {
            // 使用索引操作获取最近N个周期的最高价和最低价
            int startIndex = Math.Max(0, highPrices.Count - period);
            var lastHighs = highPrices.Skip(startIndex).Take(period).ToList(); // 最近N个周期的最高价
            var lastLows = lowPrices.Skip(startIndex).Take(period).ToList();   // 最近N个周期的最低价

            // 计算最高高点和最低低点
            double highestHigh = lastHighs.Max();
            double lowestLow = lastLows.Min();

            // 当前收盘价
            double currentClose = closingPrices.Last();

            // 避免除以零的情况（最高价等于最低价）
            if (highestHigh == lowestLow)
            {
                return (0, 0); // 如果最高价等于最低价，%K和%D均为0
            }

            // 计算%K
            double k = 100 * (currentClose - lowestLow) / (highestHigh - lowestLow);

            // 计算%D（%K的3周期SMA）
            double d = CalculateSMA(new List<double> { k }, 3);

            return (k, d);
        }

        /// <summary>
        /// 计算简单移动平均线 (SMA)。
        /// </summary>
        /// <param name="values">值列表。</param>
        /// <param name="period">计算周期。</param>
        /// <returns>返回SMA值。</returns>
        private double CalculateSMA(IList<double> values, int period)
        {
            // 使用索引操作获取最近N个周期的值
            int startIndex = Math.Max(0, values.Count - period);
            var lastValues = values.Skip(startIndex).Take(period).ToList();

            // 避免空集合的情况
            if (lastValues.Count == 0)
            {
                throw new ArgumentException("值列表为空，无法计算SMA", nameof(values));
            }

            return lastValues.Average();
        }

        /// <summary>
        /// 计算简单移动平均线 (MA)。
        /// </summary>
        /// <param name="prices">收盘价列表。</param>
        /// <param name="period">计算周期，默认为20。</param>
        /// <returns>返回移动平均值。</returns>
        private double CalculateMA(IList<double> prices, int period = 20)
        {
            if (prices == null || prices.Count == 0)
                throw new ArgumentException("价格列表不能为空", nameof(prices));
            if (period <= 0 || period > prices.Count)
                throw new ArgumentException("周期必须大于0且不超过价格列表长度", nameof(period));

            // 使用索引操作获取最近 N 个周期的价格
            int startIndex = Math.Max(0, prices.Count - period);
            var lastPrices = prices.Skip(startIndex).Take(period).ToList();

            return lastPrices.Average();
        }


        /// <summary>
        /// 执行回测并返回回测结果。
        /// </summary>
        /// <param name="klines">历史K线数据。</param>
        /// <returns>返回一个包含回测结果的元组，包括胜率、总预测次数、正确预测次数。</returns>
        private (double WinRate, int TotalPredictions, int CorrectPredictions) Backtest(JArray klines)
        {
            // 提取关键数据
            var closingPrices = klines.Select(k => (double)k[4]).ToList(); // 收盘价
            var highPrices = klines.Select(k => (double)k[2]).ToList();    // 最高价
            var lowPrices = klines.Select(k => (double)k[3]).ToList();     // 最低价
            var volumes = klines.Select(k => (double)k[5]).ToList();       // 成交量

            // 初始化回测变量
            int totalPredictions = 0;       // 总预测次数
            int correctPredictions = 0;     // 正确预测次数

            for (int i = 50; i < closingPrices.Count - 1; i++) // 跳过前50个数据点以避免不稳定的指标
            {
                // 截取到当前点的历史数据
                var currentClosingPrices = closingPrices.Take(i).ToList();
                var currentHighPrices = highPrices.Take(i).ToList();
                var currentLowPrices = lowPrices.Take(i).ToList();

                // 计算技术指标
                double rsi = CalculateRSI(currentClosingPrices, 14);
                double ma = CalculateMA(currentClosingPrices, 20);
                var bollingerBands = CalculateBollingerBands(currentClosingPrices);
                var macdResult = CalculateMACD(currentClosingPrices);
                var stochastic = CalculateStochasticOscillator(currentHighPrices, currentLowPrices, currentClosingPrices);
                double atr = CalculateATR(currentHighPrices, currentLowPrices, currentClosingPrices);
                double avgVolume = CalculateAverageVolume(new JArray(klines.Take(i)), 20);

                // 获取斐波那契回撤水平（使用最近20周期的高点和低点）
                var highestHigh = currentHighPrices.Max();
                var lowestLow = currentLowPrices.Min();
                var fibonacciRetracement = CalculateFibonacciRetracement(highestHigh, lowestLow);

                // 综合分析涨跌方向
                string trend = AnalyzeTrend(
                    currentClosingPrices.Last(),
                    rsi,
                    ma,
                    bollingerBands,
                    macdResult,
                    stochastic,
                    atr,
                    avgVolume,
                    fibonacciRetracement
                );

                // 获取实际价格变化方向
                double currentPrice = currentClosingPrices.Last();
                double nextPrice = closingPrices[i + 1];
                string actualDirection = nextPrice > currentPrice ? "上涨" : "下跌";

                // 判断预测是否正确
                if (trend == actualDirection)
                {
                    correctPredictions++;
                }

                totalPredictions++;
            }

            // 计算胜率
            double winRate = totalPredictions == 0 ? 0 : (double)correctPredictions / totalPredictions;

            // 返回回测结果
            return (winRate, totalPredictions, correctPredictions);
        }



        /// <summary>
        /// 调用币安 API 获取当前价格
        /// </summary>
        private async Task<double> GetCurrentPriceFromBinance(string symbol)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = $"https://api.binance.com/api/v3/ticker/price?symbol={symbol}";
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    JObject priceData = JObject.Parse(jsonResponse); // 使用 JSON 解析
                    return Convert.ToDouble(priceData["price"]);      // 提取价格字段
                }
                else
                {
                    throw new Exception($"API 请求失败: {response.StatusCode}");
                }
            }
        }

        /// <summary>
        /// 获取指定币种和周期的历史K线数据。
        /// </summary>
        /// <param name="symbol">币种标识符，如 "BTCUSDT"。</param>
        /// <param name="interval">K线的时间周期，如 "15m", "30m", "1h", "4h" 等。</param>
        /// <returns>返回一个JArray对象，包含K线数据。</returns>
        private async Task<JArray> GetHistoricalData(string symbol, string interval)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    string url = $"https://api.binance.com/api/v3/klines?symbol={symbol}&interval={interval}&limit=500";
                    HttpResponseMessage response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        return JArray.Parse(content);
                    }
                    else
                    {
                        analysis_Log.Text += $"获取数据失败 - 状态码: {response.StatusCode}\n";
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    analysis_Log.Text += $"获取数据时发生错误: {ex.Message}\n";
                    return null;
                }
            }
        }

        /// <summary>
        /// 计算相对强弱指数 (RSI)。
        /// </summary>
        /// <param name="prices">收盘价列表。</param>
        /// <param name="period">计算周期，默认为14。</param>
        /// <returns>返回RSI值。</returns>
        private double CalculateRSI(IList<double> prices, int period = 14)
        {
            var gains = new List<double>();
            var losses = new List<double>();

            // 遍历价格变化，计算上涨和下跌幅度
            for (int i = 1; i < prices.Count; i++)
            {
                double change = prices[i] - prices[i - 1];
                if (change > 0)
                    gains.Add(change);
                else
                    losses.Add(-change);
            }

            // 使用索引操作获取最近 N 个周期的涨幅和跌幅
            int startIndex = Math.Max(0, gains.Count - period);
            double avgGain = gains.Skip(startIndex).Take(period).Average();

            startIndex = Math.Max(0, losses.Count - period);
            double avgLoss = losses.Skip(startIndex).Take(period).Average();

            if (avgLoss == 0) return 100;
            double rs = avgGain / avgLoss;
            return 100 - (100 / (1 + rs));
        }





        /// <summary>
        /// 计算MACD指标。
        /// </summary>
        /// <param name="prices">收盘价列表。</param>
        /// <returns>返回MACD线、信号线和直方图。</returns>
        private (double MACDLine, double SignalLine, double Histogram) CalculateMACD(IList<double> prices)
        {
            // 快速EMA (12周期)
            double fastEMA = CalculateEMA(prices, 12);

            // 慢速EMA (26周期)
            double slowEMA = CalculateEMA(prices, 26);

            // MACD线 = 快速EMA - 慢速EMA
            double macdLine = fastEMA - slowEMA;

            // 信号线 (9周期EMA)
            var macdLineHistory = new List<double>();
            for (int i = 0; i < prices.Count; i++)
            {
                var tempPrices = prices.Take(i + 1).ToList();
                macdLineHistory.Add(CalculateEMA(tempPrices, 12) - CalculateEMA(tempPrices, 26));
            }

            // 使用索引操作获取最近 9 个周期的 MACD 线历史
            int startIndex = Math.Max(0, macdLineHistory.Count - 9);
            double signalLine = CalculateEMA(macdLineHistory.Skip(startIndex).Take(9).ToList(), 9);

            // 直方图 = MACD线 - 信号线
            double histogram = macdLine - signalLine;

            return (macdLine, signalLine, histogram);
        }

        /// <summary>
        /// 计算指数移动平均线 (EMA)。
        /// </summary>
        /// <param name="prices">收盘价列表。</param>
        /// <param name="period">计算周期。</param>
        /// <returns>返回给定周期的 EMA 值。</returns>
        private double CalculateEMA(IList<double> prices, int period)
        {
            if (prices == null || prices.Count == 0) throw new ArgumentException("价格列表不能为空", nameof(prices));
            if (period <= 0) throw new ArgumentException("周期必须大于0", nameof(period));

            // 平滑系数
            double smoothingMultiplier = 2.0 / (period + 1);

            // 初始EMA值为第一个价格
            double ema = prices[0];

            // 迭代计算EMA
            for (int i = 1; i < prices.Count; i++)
            {
                // EMA公式：EMA(i) = Price(i) * K + EMA(i-1) * (1 - K)
                // 其中K是平滑系数
                ema = prices[i] * smoothingMultiplier + ema * (1 - smoothingMultiplier);
            }

            return ema;
        }

        /// <summary>
        /// 计算布林带。
        /// </summary>
        /// <param name="prices">收盘价列表。</param>
        /// <param name="period">计算周期，默认为20。</param>
        /// <param name="stdDev">标准差倍数，默认为2。</param>
        /// <returns>返回布林带上轨、下轨和中轨（SMA）。</returns>
        private (double UpperBand, double LowerBand, double SMA) CalculateBollingerBands(IList<double> prices, int period = 20, double stdDev = 2)
        {
            // 使用索引操作获取最近 N 个周期的价格
            int startIndex = Math.Max(0, prices.Count - period);
            var lastPrices = prices.Skip(startIndex).Take(period).ToList();

            // 计算简单移动平均线 (SMA)
            double sma = lastPrices.Average();

            // 计算方差和标准差
            double variance = lastPrices.Select(p => Math.Pow(p - sma, 2)).Average(); // 方差
            double std = Math.Sqrt(variance); // 标准差

            // 计算布林带上轨和下轨
            double upperBand = sma + stdDev * std; // 上轨
            double lowerBand = sma - stdDev * std; // 下轨

            return (upperBand, lowerBand, sma); // 返回上轨、下轨和中轨
        }

        /// <summary>
        /// 综合涨跌方向判断
        /// </summary>
        /// <param name="currentPrice"></param>
        /// <param name="rsi"></param>
        /// <param name="ma"></param>
        /// <param name="bollingerBands"></param>
        /// <param name="macdResult"></param>
        /// <param name="stochastic"></param>
        /// <param name="atr"></param>
        /// <param name="avgVolume"></param>
        /// <param name="fibonacciRetracement"></param>
        /// <returns></returns>
        private string AnalyzeTrend(
    double currentPrice,
    double rsi,
    double ma,
    (double UpperBand, double LowerBand, double SMA) bollingerBands,
    (double MACDLine, double SignalLine, double Histogram) macdResult,
    (double K, double D) stochastic,
    double atr,
    double avgVolume,
    Dictionary<string, double> fibonacciRetracement
)
        {
            // 初始化各指标得分
            double rsiScore = 0;
            double macdScore = 0;
            double bollingerScore = 0;
            double maScore = 0;
            double atrScore = 0;
            double volumeScore = 0;
            double stochasticScore = 0;
            double fibonacciScore = 0;

            // RSI 判断
            if (rsi > 70) rsiScore = -1; // 超买，看跌
            else if (rsi < 30) rsiScore = 1; // 超卖，看涨
            else if (rsi > 50 && rsi <= 70) rsiScore = 1; // 看涨区间
            else if (rsi < 50 && rsi >= 30) rsiScore = -1; // 看跌区间

            // MACD 判断
            if (macdResult.MACDLine > macdResult.SignalLine) macdScore = 1; // 金叉，看涨
            else if (macdResult.MACDLine < macdResult.SignalLine) macdScore = -1; // 死叉，看跌

            // 布林带判断
            if (currentPrice > bollingerBands.UpperBand) bollingerScore = -1; // 接近上轨，看跌
            else if (currentPrice < bollingerBands.LowerBand) bollingerScore = 1; // 接近下轨，看涨

            // 移动平均线判断
            if (currentPrice > ma) maScore = 1; // 当前价格高于均线，看涨
            else if (currentPrice < ma) maScore = -1; // 当前价格低于均线，看跌

            // ATR 判断
            if (atr > avgVolume * 1.2) atrScore = 1; // 波动较大，可能突破，看涨
            else if (atr < avgVolume * 0.8) atrScore = -1; // 波动较小，可能盘整，看跌

            // 成交量判断
            if (avgVolume > 0 && currentPrice > ma) volumeScore = 1; // 成交量增加且价格上涨，看涨
            else if (avgVolume > 0 && currentPrice < ma) volumeScore = -1; // 成交量增加且价格下跌，看跌

            // 随机指标判断
            if (stochastic.K > stochastic.D && stochastic.K < 80) stochasticScore = 1; // 随机指标看涨
            else if (stochastic.K < stochastic.D || stochastic.K > 80) stochasticScore = -1; // 随机指标看跌

            // 斐波那契回撤判断
            if (fibonacciRetracement.ContainsKey("Support") && currentPrice < fibonacciRetracement["Support"])
                fibonacciScore = 1; // 接近支撑位，看涨
            else if (fibonacciRetracement.ContainsKey("Resistance") && currentPrice > fibonacciRetracement["Resistance"])
                fibonacciScore = -1; // 接近阻力位，看跌

            // 综合得分计算
            double totalScore =
                rsiScore * 0.25 +         // RSI 权重 25%
                macdScore * 0.25 +       // MACD 权重 25%
                bollingerScore * 0.15 +  // 布林带权重 15%
                maScore * 0.10 +         // MA 权重 10%
                atrScore * 0.10 +        // ATR 权重 10%
                volumeScore * 0.08 +     // 成交量权重 8%
                stochasticScore * 0.04 + // 随机指标权重 4%
                fibonacciScore * 0.03;   // 斐波那契权重 3%

            // 判断趋势方向
            if (totalScore > 0) return "上涨";
            else if (totalScore < 0) return "下跌";
            else return "中性(建议观望)";
        }

        /// <summary>
        /// 获取布林带的状态描述。
        /// </summary>
        /// <param name="currentPrice">当前价格。</param>
        /// <param name="bollingerBands">布林带上轨、下轨和SMA的结果。</param>
        /// <returns>返回布林带状态描述。</returns>
        private string GetBollingerBandStatus(double currentPrice, (double UpperBand, double LowerBand, double SMA) bollingerBands)
        {
            if (currentPrice > bollingerBands.UpperBand)
                return "接近上轨（可能回调）";
            else if (currentPrice < bollingerBands.LowerBand)
                return "接近下轨（可能反弹）";
            else
                return "正常范围";
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // 定义说明内容
            string instructions = @"
本工具基于以下技术指标进行综合趋势分析
1. RSI（相对强弱指数）
作用：判断市场是否超买或超卖。
规则：
RSI > 70：超买区域（看跌信号）。
RSI < 30：超卖区域（看涨信号）。
权重：25%
2. MACD（指数平滑异同移动平均线）
作用：捕捉趋势变化，识别买入和卖出信号。
规则：
MACD 线 > 信号线：金叉（看涨信号）。
MACD 线 < 信号线：死叉（看跌信号）。
权重：25%
3. 布林带
作用：判断价格波动范围，识别可能的价格反转点。
规则：
价格接近上轨：可能回调（看跌信号）。
价格接近下轨：可能反弹（看涨信号）。
权重：15%
4. MA（移动平均线）
作用：平滑价格数据，识别长期趋势。
规则：
当前价格 > MA：处于上升趋势（看涨信号）。
当前价格 < MA：处于下降趋势（看跌信号）。
权重：10%
5. ATR（平均真实波动幅度）
作用：衡量市场波动性，辅助判断趋势强度。
规则：
ATR 值较高：市场波动较大（可能有突破机会）。
ATR 值较低：市场波动较小（可能进入盘整）。
权重：10%
6. 成交量（Volume）
作用：通过成交量验证价格趋势的真实性。
规则：
成交量增加 + 价格上涨：上涨趋势确认（看涨信号）。
成交量增加 + 价格下跌：下跌趋势确认（看跌信号）。
权重：8%
7. 随机指标（Stochastic Oscillator）
作用：判断价格动量，识别超买和超卖状态。
规则：
K 线 > D 线：金叉（看涨信号）。
K 线 < D 线：死叉（看跌信号）。
%K > 80：超买区域（看跌信号）。
%K < 20：超卖区域（看涨信号）。
权重：4%
8. 斐波那契回撤水平
作用：识别关键支撑位和阻力位，判断价格回调和反弹的可能性。
规则：
价格接近 38.2%、50% 或 61.8% 回撤水平：可能出现支撑或阻力。
如果价格突破回撤水平：可能继续当前趋势。
权重：3%

综合趋势分析逻辑
为了更全面地评估市场趋势，我们对所有技术指标进行了加权计算，得出最终的趋势方向（上涨或下跌）。
";

            // 弹出消息框
            MessageBox.Show(instructions, "依据指标说明", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        /// <summary>
        /// 初始化倒计时计时器
        /// </summary>
        private void InitializeCountdownTimer()
        {
            timer1 = new Timer(); // 确保计时器实例化
            timer1.Interval = 1000; // 每秒触发一次
            timer1.Tick += timer1_Tick; // 绑定 Tick 事件处理程序
        }

        private async void timer1_Tick(object sender, EventArgs e)
        {
            remainingSeconds = (int)timer1.Tag;

            if (remainingSeconds > 0)
            {
                remainingSeconds--;
                timer1.Tag = remainingSeconds;

                // 更新 Label 显示倒计时
                UpdateCountdownLabel(remainingSeconds);
            }
            else
            {
                // 倒计时结束，停止计时器
                timer1.Stop();

                // 获取最新价格
                string selectedCurrency = com_Currency.SelectedItem.ToString().Replace("/", "");
                double latestPrice = await GetCurrentPriceFromBinance(selectedCurrency);
                analysis_Log.Text += $"倒计时结束后价格: {latestPrice:F2}\n";
                analysis_Log.Text += Environment.NewLine;

                // 判断 W 或 L 状态
                string result = DetermineWinOrLoss(latestPrice);
                analysis_Log.Text += $"结果: {result}\n";
                analysis_Log.Text += Environment.NewLine;

                // 更新 Label 显示倒计时结束
                lbl_Countdown.Text = "倒计时结束";
            }
        }


        /// <summary>
        /// 判断 W 或 L 状态
        /// </summary>
        private string DetermineWinOrLoss( double latestPrice)
        {
            // 综合趋势方向
            if (predictedTrend == "上涨")
            {
                // 如果趋势为上涨，且最新价格大于初始价格，则为 W
                return latestPrice > initialPrice ? "W" : "L";
            }
            else if (predictedTrend == "下跌")
            {
                // 如果趋势为下跌，且最新价格小于初始价格，则为 W
                return latestPrice < initialPrice ? "W" : "L";
            }
            else
            {
                // 如果趋势方向无效，默认为 L
                return "L";
            }
        }

    }
}
