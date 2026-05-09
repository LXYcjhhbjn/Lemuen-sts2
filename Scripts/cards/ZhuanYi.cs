using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace LXY.Scripts;

[Pool(typeof(lemuenCardPool))]
public class ZhuanYi: CustomCardModel
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<AIM>(4m)];
    public override string PortraitPath => $"res://lemuen/images/cards/{Id.Entry.ToLowerInvariant()}.png";
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
		HoverTipFactory.FromPower<AIM>(),
        HoverTipFactory.FromPower<AMMO>()
    ];
	public ZhuanYi() : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true)
	{
	}

	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
	{
		await PowerCmd.Apply<AMMO>(choiceContext,base.Owner.Creature, base.Owner.Creature.GetPowerAmount<AIM>() / base.DynamicVars["AIM"].IntValue, base.Owner.Creature, this);
        await PowerCmd.Remove<AIM>(base.Owner.Creature);
	}

	protected override void OnUpgrade()
	{
		base.DynamicVars["AIM"].UpgradeValueBy(-1);
	}
}