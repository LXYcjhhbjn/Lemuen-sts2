using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace LXY.Scripts;

[Pool(typeof(lemuenCardPool))]
public class DanYaoZhengBei: CustomCardModel
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<AMMO>(1m),new CardsVar(1)];
    public override string PortraitPath => $"res://lemuen/images/cards/{Id.Entry.ToLowerInvariant()}.png";
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
		HoverTipFactory.FromPower<AMMO>()
    ];	
	public DanYaoZhengBei()
		: base(1, CardType.Skill, CardRarity.Common, TargetType.Self,true)
	{
    }
	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
	{
		await CardPileCmd.Draw(choiceContext, base.DynamicVars.Cards.BaseValue, base.Owner);
		await PowerCmd.Apply<AMMO>(choiceContext , base.Owner.Creature, base.DynamicVars["AMMO"].BaseValue, base.Owner.Creature, this);
	}
	protected override void OnUpgrade()
	{
        base.DynamicVars["AMMO"].UpgradeValueBy(1);
		base.DynamicVars.Cards.UpgradeValueBy(1);
	}
}