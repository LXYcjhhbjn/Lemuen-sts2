# Lemuen - 杀戮尖塔2 角色MOD

[![Godot 4.5](https://img.shields.io/badge/Godot-4.5-blue.svg)](https://godotengine.org/)
[![C#](https://img.shields.io/badge/Language-C%23-green.svg)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![STS2](https://img.shields.io/badge/Game-Slay%20the%20Spire%202-red.svg)](https://store.steampowered.com/app/646570/Slay_the_Spire_2/)

## 简介

这是一个为《杀戮尖塔2》(Slay the Spire 2) 开发的自定义角色MOD，添加了来自《明日方舟》的角色 **蕾缪安 (Lemuen)**。

蕾缪安是一名使用枪械和弹药系统的远程攻击角色，拥有独特的"弹药装填"和"改装"机制。

## 特性

- **初始生命值**: 75

### 核心机制
- **弹药**: 独特的弹药装填和消耗机制
- **改装**: 独特的改装牌，每回合产生效果

### 初始卡组
- 射击 (SheJi) x1
- 聚精会神 (JuJingHuiShen) x1
- 弹药装填 (DanYaoZhuangTian) x4
- 寻找掩体 (XunZhaoYanTi) x4

### 初始遗物
- **GunFlowers** - 蕾缪安的专属起始遗物

- **游戏引擎**: Godot 4.5
- **编程语言**: C#
- **音频系统**: FMOD
- **MOD框架**: Harmony (用于代码注入)

## 项目结构

```
lemuen/
├── Scripts/                    # C# 脚本
│   ├── Entry.cs               # MOD入口点
│   ├── character/             # 角色定义
│   │   └── lemuenCharacter.cs
│   ├── cards/                 # 卡牌实现
│   ├── powers/                # 能力实现
│   ├── relics/                # 遗物实现
│   ├── potions/               # 药水实现
│   └── pools/                 # 卡池/遗物池/药水池
├── lemuen/
│   ├── scenes/                # 场景文件 (.tscn)
│   ├── images/                # 图片资源
│   └── sfx/                   # 音效资源
└── addons/                    # Godot插件
    └── fmod/                  # FMOD音频插件
```

## 安装方法

1. 确保已安装《杀戮尖塔2》游戏
2. 将本MOD文件夹复制到游戏的MOD目录
3. 启动游戏，在角色选择界面选择"蕾缪安"

## 开发信息

### 依赖项
- Godot 4.5+
- .NET SDK
- FMOD Studio


## 许可证

本项目仅供学习和个人使用。角色形象版权归属于《明日方舟》开发商。

## 致谢

- 《杀戮尖塔2》开发团队 Mega Crit
- 《明日方舟》开发商 鹰角网络
- Godot 引擎社区

---

*Made with ❤️ for Slay the Spire 2*
