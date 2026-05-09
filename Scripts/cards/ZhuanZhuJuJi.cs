using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace LXY.Scripts;

[Pool(typeof(lemuenCardPool))]
public class ZhuanZhuJuJi: CustomCardModel
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<AIM>(3m),new CardsVar(2)];
    public override string PortraitPath => $"res://lemuen/images/cards/{Id.Entry.ToLowerInvariant()}.png";
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
		HoverTipFactory.FromPower<AIM>()
    ];
	public ZhuanZhuJuJi()
		: base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self,true)
	{
    }
	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
	{
		await CardPileCmd.Draw(choiceContext, base.DynamicVars.Cards.BaseValue, base.Owner);
		await PowerCmd.Apply<AIM>(choiceContext,base.Owner.Creature, base.DynamicVars["AIM"].BaseValue, base.Owner.Creature, this);
	}
	protected override void OnUpgrade()
	{
		base.DynamicVars["AIM"].UpgradeValueBy(2m);
        base.DynamicVars.Cards.UpgradeValueBy(1);
	}
}