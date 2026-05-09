using BaseLib.Abstracts;
using BaseLib.Utils;
using Godot;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Context;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;

namespace LXY.Scripts;

[Pool(typeof(lemuenCardPool))]
public class GongGan: CustomCardModel
{
    public override CardMultiplayerConstraint MultiplayerConstraint => CardMultiplayerConstraint.MultiplayerOnly;
    protected override IEnumerable<DynamicVar> CanonicalVars => [new CardsVar(1)];
	public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust,CardKeyword.Retain];
    public override string PortraitPath => $"res://lemuen/images/cards/{Id.Entry.ToLowerInvariant()}.png";
	protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromKeyword(MyKeywords.Modification)
    ];
	public GongGan(): base(0, CardType.Skill, CardRarity.Rare, TargetType.AllAllies , true)
	{
    }
	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
	{
        await CardPileCmd.Draw(choiceContext, 1 , base.Owner);
		IEnumerable<Creature> enumerable = from c in base.CombatState.GetTeammatesOf(base.Owner.Creature)where c != null && c.IsAlive && c.IsPlayer select c;
         foreach (Creature creature in enumerable)
		{     
            List<CardModel> cardModel = CardFactory.GetForCombat(creature.Player, from c in ModelDb.CardPool<TokenCardPool>().GetUnlockedCards(creature.Player.UnlockState, creature.Player.RunState.CardMultiplayerConstraint)
			    where c.Keywords.Contains(MyKeywords.Modification)
			    select c, 2, base.Owner.RunState.Rng.CombatCardGeneration).ToList();
		    if (cardModel != null)
		    {
		        IReadOnlyList<CardPileAddResult> results = await CardPileCmd.AddGeneratedCardsToCombat(cardModel, PileType.Hand, cardPlay.Card.Owner);
				if (LocalContext.IsMe(creature))
				{
					CardCmd.PreviewCardPileAdd(results);
				}
            }
        }
	}
	protected override void OnUpgrade()
	{
		AddKeyword(CardKeyword.Innate);
	}
}
