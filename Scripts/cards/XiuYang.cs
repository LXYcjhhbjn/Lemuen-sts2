using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;

namespace LXY.Scripts;

[Pool(typeof(lemuenCardPool))]
public class XiuYang: CustomCardModel
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new CardsVar(1),new PowerVar<StrengthPower>(1m),new HealVar(3m)];
    public override string PortraitPath => $"res://lemuen/images/cards/{Id.Entry.ToLowerInvariant()}.png";
	protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<StrengthPower>()];	
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];
	public XiuYang()
		: base(0, CardType.Skill, CardRarity.Uncommon, TargetType.Self , true)
	{
    }
	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
	{
        await PowerCmd.Apply<DrawCardsNextTurnPower>(choiceContext,base.Owner.Creature, base.DynamicVars.Cards.BaseValue, base.Owner.Creature, this);
		await PowerCmd.Apply<StrengthPower>(choiceContext,base.Owner.Creature, base.DynamicVars["StrengthPower"].BaseValue, base.Owner.Creature, this);
        await CreatureCmd.Heal(base.Owner.Creature, base.DynamicVars.Heal.BaseValue);
	}
	protected override void OnUpgrade()
	{
		base.DynamicVars.Heal.UpgradeValueBy(1m);
	}
}
