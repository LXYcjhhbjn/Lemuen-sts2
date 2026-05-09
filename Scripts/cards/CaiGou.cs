using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;

namespace LXY.Scripts;

[Pool(typeof(lemuenCardPool))]
public class CaiGou: CustomCardModel
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new CardsVar(2)];
	public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];
    public override string PortraitPath => $"res://lemuen/images/cards/{Id.Entry.ToLowerInvariant()}.png";
	protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromKeyword(MyKeywords.Modification)
    ];
	public CaiGou(): base(2, CardType.Skill, CardRarity.Uncommon, TargetType.Self,true)
	{
    }
	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
	{
        List<CardModel> cardModel = CardFactory.GetForCombat(base.Owner.Creature.Player, from c in ModelDb.CardPool<TokenCardPool>().GetUnlockedCards(base.Owner.Creature.Player.UnlockState, base.Owner.Creature.Player.RunState.CardMultiplayerConstraint)
			where c.Keywords.Contains(MyKeywords.Modification)
			select c,base.DynamicVars.Cards.IntValue , base.Owner.RunState.Rng.CombatCardGeneration).ToList();
		if (cardModel != null)
		{
		    IReadOnlyList<CardPileAddResult> results = await CardPileCmd.AddGeneratedCardsToCombat(cardModel, PileType.Exhaust, cardPlay.Card.Owner);
			CardCmd.PreviewCardPileAdd(results);
        }
	}
	protected override void OnUpgrade()
	{
		base.EnergyCost.UpgradeBy(-1);
	}
}
