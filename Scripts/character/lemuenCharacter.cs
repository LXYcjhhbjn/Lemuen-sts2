using BaseLib.Abstracts;
using Godot;
using MegaCrit.Sts2.Core.Entities.Characters;
using MegaCrit.Sts2.Core.Models;

namespace LXY.Scripts;
public class lemuenCharacter : PlaceholderCharacterModel
{
    // 角色名称颜色
    public override Color NameColor => new(1f, 0.7f, 0.9f);
    // 能量图标轮廓颜色
    public override Color EnergyLabelOutlineColor => new(0.722f, 0.451f, 0.2f);

    // 人物性别（男女中立）
    public override CharacterGender Gender => CharacterGender.Feminine;
   
    // 初始血量
    public override int StartingHp => 75;

    // 人物模型tscn路径。要自定义见下。
    public override string CustomVisualPath => "res://lemuen/scenes/lemuen_character2.tscn";
    // 卡牌拖尾路径。
    // public override string CustomTrailPath => "res://scenes/vfx/card_trail_ironclad.tscn";
    // 人物头像路径。
    public override string CustomIconTexturePath => "res://lemuen/images/icon.svg";
    // 人物头像2号。
    public override string CustomIconPath => "res://lemuen/scenes/lemuen_icon.tscn";
    // 能量表盘tscn路径。要自定义见下。
    public override string CustomEnergyCounterPath => "res://lemuen/scenes/lemuen_energy_counter.tscn";
    // 篝火休息动画。
    public override string CustomRestSiteAnimPath => "res://lemuen/scenes/lemuen_rest_site.tscn";
    // 商店人物动画。
    public override string CustomMerchantAnimPath => "res://lemuen/scenes/lemuen_merchant.tscn";
    // 多人模式-手指。
    public override string CustomArmPointingTexturePath => "res://lemuen/images/ping.png";
    // 多人模式剪刀石头布-石头。
    public override string CustomArmRockTexturePath => "res://lemuen/images/rock.png";
    // 多人模式剪刀石头布-布。
    public override string CustomArmPaperTexturePath => "res://lemuen/images/paper.png";
    // 多人模式剪刀石头布-剪刀。
    public override string CustomArmScissorsTexturePath => "res://lemuen/images/ser.png";

    // 人物选择背景。
    public override string CustomCharacterSelectBg => "res://lemuen/scenes/lemuen_bg.tscn";
    // 人物选择图标。
    public override string CustomCharacterSelectIconPath => "res://lemuen/images/char_select_lemuen.png";
    // 人物选择图标-锁定状态。
    public override string CustomCharacterSelectLockedIconPath => "res://lemuen/images/char_select_lemuen_locked.png";
    // 人物选择过渡动画。
    // public override string CustomCharacterSelectTransitionPath => "res://materials/transitions/ironclad_transition_mat.tres";
    // 地图上的角色标记图标、表情轮盘上的角色头像
    public override string CustomMapMarkerPath => "res://lemuen/images/icon.svg";
    // 攻击音效
    public override string CustomAttackSfx => "res://lemuen/sfx/attack.wav";
    // 施法音效
    public override string CustomCastSfx => "res://lemuen/sfx/cast.wav";
    // 死亡音效
    public override string CustomDeathSfx => "res://lemuen/sfx/die.wav";
    // 角色选择音效
    public override string CharacterSelectSfx => "res://lemuen/sfx/select.wav";
    // 过渡音效。这个不能删。
    public override string CharacterTransitionSfx => "event:/sfx/ui/wipe_ironclad";

    public override CardPoolModel CardPool => ModelDb.CardPool<lemuenCardPool>();
    public override RelicPoolModel RelicPool => ModelDb.RelicPool<lemuenRelicPool>();
    public override PotionPoolModel PotionPool => ModelDb.PotionPool<lemuenPotionPool>();

    // 初始卡组
    public override IEnumerable<CardModel> StartingDeck => [
        ModelDb.Card<SheJi>(),
        ModelDb.Card<JuJIngHuiShen>(),
        ModelDb.Card<DanYaoZhuangTian>(),
        ModelDb.Card<DanYaoZhuangTian>(),
        ModelDb.Card<DanYaoZhuangTian>(),
        ModelDb.Card<DanYaoZhuangTian>(),
        ModelDb.Card<XunZhaoYanTi>(),
        ModelDb.Card<XunZhaoYanTi>(),
        ModelDb.Card<XunZhaoYanTi>(),
        ModelDb.Card<XunZhaoYanTi>(),
    ];

    // 初始遗物
    public override IReadOnlyList<RelicModel> StartingRelics => [
        ModelDb.Relic<GunFlowers>()
    ];

    // 攻击建筑师的攻击特效列表
    
    public override List<string> GetArchitectAttackVfx() => [
        "vfx/vfx_attack_blunt",
        "vfx/vfx_heavy_blunt",
        "vfx/vfx_attack_slash",
        "vfx/vfx_bloody_impact",
        "vfx/vfx_rock_shatter"
    ];
}