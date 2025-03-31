using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jmsTools.Manage
{
    public class EthAddressDifficultyCalculator
    {
        // 计算匹配特定前缀和/或后缀的ETH地址的难度
        public static resultItem CalculateDifficulty(string prefix = "", string suffix = "",int threadCount=8)
        {
            // 获取前缀和后缀长度
            int prefixLength = string.IsNullOrEmpty(prefix) ? 0 : prefix.Length;
            int suffixLength = string.IsNullOrEmpty(suffix) ? 0 : suffix.Length;

            // 计算复杂度
            double complexity = Math.Pow(16, prefixLength + suffixLength);

            // 假设每个线程每秒可以生成 50 个地址
            int addressesPerSecondPerThread = 50;
            int totalAddressesPerSecond = threadCount * addressesPerSecondPerThread;

            // 计算所需时间（秒）
            double estimatedTimeInSeconds = complexity / totalAddressesPerSecond;

            // 转换时间为更易读的格式
            string estimatedTime = FormatTime(estimatedTimeInSeconds);

            resultItem rs = new resultItem();
            rs.complexity = complexity;
            rs.estimatedTime = estimatedTime;
            return rs;
        }



        public static string FormatTime(double seconds)
        {
            if (seconds < 60)
            {
                return $"{seconds:F2} 秒";
            }
            else if (seconds < 3600)
            {
                double minutes = seconds / 60;
                return $"{minutes:F2} 分钟";
            }
            else if (seconds < 86400)
            {
                double hours = seconds / 3600;
                return $"{hours:F2} 小时";
            }
            else
            {
                double days = seconds / 86400;
                return $"{days:F2} 天";
            }
        }
    }

    public class resultItem {
    
        public double complexity { get; set; }
        public string estimatedTime { get; set; }
    }
}
