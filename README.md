# Bleatingsheep.IPLocation
IP 地址转地理信息。支持双栈，自动识别部分隧道。

## 前提条件
你的项目必须支持 .NET Standard 2.0。

## 如何使用
### 添加 nuget 引用
略。
### 编写代码
```C#
var (success, result) = await Bleatingsheep.IPLocation.IPLocator.Default.GetLocationAsync("8.8.8.8");
Console.WriteLine(result);
```

## 数据源
IPv6：[ZX](http://ip.zxinc.org/ipquery)

IPv4：[IPIP](https://www.ipip.net/ip.html) [免费 API](https://www.ipip.net/support/api.html)

## 其他
### IPv6 隧道及映射地址自动识别
请求 IPv6 地址的地理位置信息时，会自动识别地址是否为 Teredo 隧道地址（2001::/32）、6to4 隧道地址（2002::/16）或者 IPv4 到 IPv6 映射地址（::ffff:0.0.0.0/96）。如果是，则会提取出 IPv4 地址进行查询。
### IPIP 频率限制
IPIP.net 免费 API 频率限制较严格，只有 1 秒几次。因此，我在代码里进行了判断，在过于频繁的情况下会在约 110 毫秒后重试，**直到成功**。

IPIP 说免费 API 一天限制 1000 次，关于这一点我暂时没有测试。我不确定 IPIP 在已经阻止了过高频率请求的情况下，是否真的对每日总次数做出了限制。所以，如果你有大量 IP 需要查询，请谨慎。

## 使用自己指定的数据源
你可以编写代码，自己指定 IPv4 或者 IPv6 的数据源。**将在未来版本中支持。**