using Solana.Unity.Rpc.Messages;
using Solana.Unity.Rpc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;
using System.Numerics;
using Solana.Unity.Rpc.Core.Http;
using Nethereum.Web3;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace jmsTools.Manage
{

    public class RpcClientManager
    {
        private List<string> rpcUrls;
        private Random random = new Random();




   
        public RpcClientManager(string filePath)
        {
            rpcUrls = File.ReadAllLines(filePath).ToList();
        }

        public string GetRandomRpcUrl()
        {
            return rpcUrls[random.Next(rpcUrls.Count)];
        }
    }

    public static class BlockManage
    {

        /// <summary>
        /// 根据主网查询交易次数,SOL链不直接提供交易次数接口，所以直接查余额,用于碰撞
        /// </summary>
        /// <param name="address"></param>
        /// <param name="selectedNetwork"></param>
        /// <returns></returns>
        public static async Task<decimal?> GetBalanceInfoByPz(string address, string selectedNetwork)
        {
            switch (selectedNetwork.ToUpper())
            {
                case "ETH":
                case "BSC":
                    // 对于以太坊和币安智能链，保持原有逻辑不变
                    string[] rpcNodes;
                    if (selectedNetwork.ToUpper() == "ETH")
                    {
                        rpcNodes = File.ReadAllLines("Ethrpc_nodes.txt");
                    }
                    else // BSC
                    {
                        rpcNodes = File.ReadAllLines("Bscrpc_nodes.txt");
                    }

                    using (var client = new HttpClient())
                    {
                        var url = rpcNodes[new Random().Next(rpcNodes.Length)];


                        Web3 _web3 = new Web3(url);

                        var payload = new
                        {
                            jsonrpc = "2.0",
                            method = GetMethodByMainNet(selectedNetwork),
                            @params = GetParamsByMainNet(selectedNetwork, address),
                            id = 1
                        };

                        var jsonString = System.Text.Json.JsonSerializer.Serialize(payload);
                        var stringContent = new StringContent(jsonString, System.Text.Encoding.UTF8, "application/json");

                        var response = await client.PostAsync(url, stringContent);

                        if (response.IsSuccessStatusCode)
                        {
                            var jsonResponse = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(await response.Content.ReadAsStringAsync());
                            // 结果是以wei为单位的十六进制字符串形式返回
                            var balanceInWeiHex = jsonResponse.GetProperty("result").GetString();

                            // 将十六进制字符串转换为BigInteger
                            bool success = BigInteger.TryParse(balanceInWeiHex.Substring(2), NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture, out BigInteger balanceInWei);
                            if (!success)
                            {
                                
                                return null;
                            }

                            decimal balanceInEtherOrBnb = Web3.Convert.FromWei(balanceInWei); // 使用通用转换方法
                            return balanceInEtherOrBnb > 0 ? balanceInEtherOrBnb : 0;
                        }
                        else
                        {
                        }
                    }
                    break;

                case "SOL":

                    var rpcManager = new RpcClientManager("Solrpc_nodes.txt");
                    var rpcUrl = rpcManager.GetRandomRpcUrl();//随机使用rpc，防止单一rpc请求限制

                    // 对于Solana，使用Solana.Unity.Rpc查询余额
                    var rpcClient = ClientFactory.GetClient(rpcUrl); // 或者Cluster.Testnet, Cluster.Devnet等

                    

                    try
                    {
                        var balanceResponse = await rpcClient.GetBalanceAsync(address);


                        if (balanceResponse.WasSuccessful)
                        {
                            // 确保balanceResponse.Result是可以进行除法运算的数值类型
                            if (balanceResponse.Result is ResponseValue<ulong> lamports)
                            {
                                decimal solBalance = lamports.Value / (decimal)1e9; // 转换为SOL
                                return solBalance > 0 ? solBalance : 0;
                            }
                            else
                            {
                                return null;
                            }
                        }
                        else
                        {
                            return null; // 查询失败时返回null
                        }
                    }
                    catch (Exception ex)
                    {
                        return null; // 错误发生时返回null
                    }
                    break;

                default:
                    throw new NotSupportedException("不支持的网络类型");
            }

            return null;
        }


        /// <summary>
        /// 根据主网查询交易次数,SOL链不直接提供交易次数接口，所以直接查余额, 其他链还继续查交易记录等信息，用于单地址查询
        /// </summary>
        /// <param name="address"></param>
        /// <param name="selectedNetwork"></param>
        /// <returns></returns>
        public static async Task<(decimal? Balance, List<TransactionRecord> Transactions)> GetBalanceInfo(string address, string selectedNetwork)
        {

            decimal? balance = null;
            int transactionCount = 0;
            var transactions = new List<TransactionRecord>();

            switch (selectedNetwork.ToUpper())
            {
                case "ETH":
                case "BSC":
                    // 对于以太坊和币安智能链，保持原有逻辑不变
                    string[] rpcNodes;
                    if (selectedNetwork.ToUpper() == "ETH")
                    {
                        rpcNodes = File.ReadAllLines("Ethrpc_nodes.txt");
                    }
                    else // BSC
                    {
                        rpcNodes = File.ReadAllLines("Bscrpc_nodes.txt");
                    }

                    using (var client = new HttpClient())
                    {
                        var url = rpcNodes[new Random().Next(rpcNodes.Length)];


                        Web3 _web3 = new Web3(url);

                     

                        var payload = new
                        {
                            jsonrpc = "2.0",
                            method = GetMethodByMainNet(selectedNetwork),
                            @params = GetParamsByMainNet(selectedNetwork, address),
                            id = 1
                        };

                        var jsonString = System.Text.Json.JsonSerializer.Serialize(payload);
                        var stringContent = new StringContent(jsonString, System.Text.Encoding.UTF8, "application/json");

                        var response = await client.PostAsync(url, stringContent);

                        if (response.IsSuccessStatusCode)
                        {
                            var jsonResponse = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(await response.Content.ReadAsStringAsync());
                            // 结果是以wei为单位的十六进制字符串形式返回
                            var balanceInWeiHex = jsonResponse.GetProperty("result").GetString();

                            // 将十六进制字符串转换为BigInteger
                            bool success = BigInteger.TryParse(balanceInWeiHex.Substring(2), NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture, out BigInteger balanceInWei);
                            if (!success)
                            {

                                return (balance, transactions);
                            }

                            balance = Web3.Convert.FromWei(balanceInWei); // 使用通用转换方法
                        }
                        // 获取交易记录
                        transactions = await GetTransactionRecordsForEthOrBsc(_web3, address);
                        transactionCount = transactions.Count;
                    }
                    break;

                case "SOL":

                    var rpcManager = new RpcClientManager("Solrpc_nodes.txt");
                    var rpcUrl = rpcManager.GetRandomRpcUrl();//随机使用rpc，防止单一rpc请求限制

                    // 对于Solana，使用Solana.Unity.Rpc查询余额
                    var rpcClient = ClientFactory.GetClient(rpcUrl); // 或者Cluster.Testnet, Cluster.Devnet等



                    try
                    {
                        var balanceResponse = await rpcClient.GetBalanceAsync(address);


                        if (balanceResponse.WasSuccessful)
                        {
                            // 确保balanceResponse.Result是可以进行除法运算的数值类型
                            if (balanceResponse.Result is ResponseValue<ulong> lamports)
                            {
                                decimal solBalance = lamports.Value / (decimal)1e9; // 转换为SOL
                                //return solBalance > 0 ? solBalance : 0;

                                return (solBalance, transactions);

                            }
                            else
                            {
                                return (balance, transactions);
                            }
                        }
                        else
                        {
                            //return null; // 查询失败时返回null
                        }
                    }
                    catch (Exception ex)
                    {
                        //return null; // 错误发生时返回null
                        return (balance, transactions);
                    }
                    break;

                default:
                    throw new NotSupportedException("不支持的网络类型");
            }

            return (balance, transactions);
        }



        /// <summary>
        /// 查询ETH及BSC链的交易记录逻辑，过滤合约交互及代币转移，仅保留链上转账交易
        /// </summary>
        /// <param name="web3"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        private static async Task<List<TransactionRecord>> GetTransactionRecordsForEthOrBsc(Web3 web3, string address)
        {
            var transactions = new List<TransactionRecord>();
            var blockNumberHex = await web3.Eth.Blocks.GetBlockNumber.SendRequestAsync();
            // 使用Value属性获取ulong类型的最新区块号
            var latestBlockNumber = (ulong)blockNumberHex.Value;

            // 定义起始块和结束块（这里我们查询最新块之前的500个块作为示例）
            var startBlock = Math.Max(0, latestBlockNumber - 200);
            var endBlock = latestBlockNumber;


            // 使用列表存储所有要执行的任务
            var tasks = new List<Task>();
            for (var block = startBlock; block <= endBlock; block++)
            {
                tasks.Add(Task.Run(async () => {

                    try
                    {
                        var blockWithTransactions = await web3.Eth.Blocks.GetBlockWithTransactionsByNumber.SendRequestAsync(new Nethereum.Hex.HexTypes.HexBigInteger(block));

                        if (blockWithTransactions!=null)
                        {
                            foreach (var transaction in blockWithTransactions.Transactions)
                            {

                                // 过滤掉合约交互，只保留普通转账
                                if ((transaction.From.Equals(address, StringComparison.OrdinalIgnoreCase) ||
                                     (!string.IsNullOrEmpty(transaction.To) && transaction.To.Equals(address, StringComparison.OrdinalIgnoreCase))) &&
                                    (transaction.Input == "0x"))
                                {

                                    // 获取区块时间戳并转换为DateTime
                                    var timestampInSeconds = blockWithTransactions.Timestamp.Value;
                                    var blockTimestamp = DateTimeOffset.FromUnixTimeSeconds((long)timestampInSeconds).UtcDateTime;

                                    var txRecord = new TransactionRecord
                                    {
                                        TxHash = transaction.BlockHash,
                                        Timestamp = blockTimestamp,
                                        Value = Web3.Convert.FromWei(transaction.Value),
                                        From = transaction.From,
                                        To = transaction.To
                                    };

                                    lock (transactions) // 确保线程安全
                                    {
                                        transactions.Add(txRecord);
                                    }
                                }

                            }
                        }
                       
                    }
                    catch (Exception ex)
                    {
                        // 错误处理：可以记录日志或者进行其他处理
                        Console.WriteLine($"Error fetching block {block}: {ex.Message}");
                    }

                }));


              
            }

            // 并发执行所有任务
            await Task.WhenAll(tasks);

            return transactions;
        }


        // 根据主网获取对应的RPC方法名
        public static string GetMethodByMainNet(string mainNet)
        {
            switch (mainNet.ToLower())
            {
                case "eth":
                case "bsc":
                    return "eth_getBalance";
                case "sol":
                    return "getTransactionCount"; // Solana的具体方法名称需要根据实际情况调整
                default:
                    return "eth_getBalance";
            }
        }

        // 根据主网获取对应的RPC请求参数
        public static object[] GetParamsByMainNet(string mainNet, string address)
        {
            switch (mainNet.ToLower())
            {
                case "eth":
                case "bsc":
                    return new object[] { address, "latest" };
                case "sol":
                    return new object[] { address }; // Solana的具体参数格式需要根据实际情况调整
                default:
                    return new object[] { address, "latest" };
            }
        }



        /// <summary>
        /// 多链查询
        /// </summary>
        /// <param name="address"></param>
        /// <param name="network"></param>
        /// <returns></returns>
        public static async Task<BlockchainData> FullAddressQuery(string address, string network)
        {
            var data = new BlockchainData();

            // 并发执行基础查询 
            var balanceTask =await GetBalanceInfo(address, network);

            data.Balance = (decimal)balanceTask.Balance;
            data.Transactions = balanceTask.Transactions;
            data.TransactionCount = balanceTask.Transactions.Count;
            return data;
        }
    }
}
