using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace LXY.Scripts;

[Pool(typeof(lemuenCardPool))]
public class ZhanQianZhunBei: CustomCardModel
{
  public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];
  protected override IEnumerable<DynamicVar> CanonicalVars => [new EnergyVar(1),new CardsVar(1)];
  public override string PortraitPath => $"res://lemuen/images/cards/{Id.Entry.ToLowerInvariant()}.png";
  protected override IEnumerable<IHoverTip> ExtraHoverTips => [
	  HoverTipFactory.FromPower<AMMO>()
  ];
  public ZhanQianZhunBei() : base(0, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
  {
  }
	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
	{
    await PowerCmd.Apply<AMMO>(choiceContext,base.Owner.Creature, 1, base.Owner.Creature, this);
    await CardPileCmd.Draw(choiceContext, base.DynamicVars.Cards.BaseValue, base.Owner);
	  await PlayerCmd.GainEnergy(base.DynamicVars.Energy.BaseValue, base.Owner);
	}
	protected override void OnUpgrade()
	{
		RemoveKeyword(CardKeyword.Exhaust);
	}
}