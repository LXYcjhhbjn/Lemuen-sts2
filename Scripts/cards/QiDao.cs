using BaseLib.Abstracts;
using BaseLib.Utils;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace LXY.Scripts;

[Pool(typeof(lemuenCardPool))]
public class QiDao: CustomCardModel
{
  public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];
  protected override IEnumerable<DynamicVar> CanonicalVars => [new EnergyVar(1),new CardsVar(1)];
  public override string PortraitPath => $"res://lemuen/images/cards/{Id.Entry.ToLowerInvariant()}.png";
  public QiDao() : base(0, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
  {
  }
	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
	{
        await PlayerCmd.GainEnergy(base.DynamicVars.Energy.BaseValue, base.Owner);
        await CardPileCmd.Draw(choiceContext, base.DynamicVars.Cards.BaseValue, base.Owner);
        List<PowerModel> list1=base.Owner.Creature.Powers.ToList();
        foreach (PowerModel item in list1)
        {
            if (item.TypeForCurrentAmount == PowerType.Debuff && !(item is ITemporaryPower))
            {
                await PowerCmd.Remove(item);
                return;
            }
        }
	}
	protected override void OnUpgrade()
	{
		base.DynamicVars.Energy.UpgradeValueBy(1);
	}
}