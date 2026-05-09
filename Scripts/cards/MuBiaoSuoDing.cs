using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace LXY.Scripts;

[Pool(typeof(lemuenCardPool))]
public class MuBiaoSuoDing: CustomCardModel
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<AIM>(4m),
        new PowerVar<ZhuanZhu>(4m)
        ];
    public override string PortraitPath => $"res://lemuen/images/cards/{Id.Entry.ToLowerInvariant()}.png";
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
		HoverTipFactory.FromPower<AIM>(),
        HoverTipFactory.FromPower<ZhuanZhu>()
    ];
    public MuBiaoSuoDing() : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
    {
    }
	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
	{
        await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
        await PowerCmd.Apply<AIM>(choiceContext , base.Owner.Creature, base.DynamicVars["AIM"].BaseValue, base.Owner.Creature, this);
        await PowerCmd.Apply<ZhuanZhu>(choiceContext , base.Owner.Creature, base.DynamicVars["ZhuanZhu"].BaseValue, base.Owner.Creature, this);
	}
	protected override void OnUpgrade()
	{
		base.DynamicVars["AIM"].UpgradeValueBy(3m);
	}
}