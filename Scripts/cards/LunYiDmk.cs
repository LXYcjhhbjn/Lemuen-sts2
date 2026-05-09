using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.ValueProps;

namespace LXY.Scripts;

[Pool(typeof(TokenCardPool))]
public class LunYiDmk: CustomCardModel
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(5m, ValueProp.Unpowered | ValueProp.Move),new BlockVar(5m, ValueProp.Unpowered | ValueProp.Move)];
    public override string PortraitPath => $"res://lemuen/images/cards/{Id.Entry.ToLowerInvariant()}.png";
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Unplayable,MyKeywords.Modification];
	public LunYiDmk() : base(-1, CardType.Skill, CardRarity.Rare, TargetType.None ,true){}
	protected override void OnUpgrade()
	{
		base.DynamicVars.Damage.UpgradeValueBy(3m);
		base.DynamicVars.Block.UpgradeValueBy(3m);
	}
	public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext,CombatSide side)
	{
		CardPile?pile = base.Pile;
		if (pile != null && pile.Type != PileType.Exhaust && side == base.Owner.Creature.Side)
		{
			await CardPileCmd.Add(this,PileType.Exhaust);
        	if (base.Owner.RunState.Rng.CombatTargets.NextItem(base.Owner.Creature.CombatState.HittableEnemies) != null)
        	{
		    	await CreatureCmd.Damage(choiceContext, base.Owner.RunState.Rng.CombatTargets.NextItem(base.Owner.Creature.CombatState.HittableEnemies), base.DynamicVars.Damage, base.Owner.Creature);
        	}	
			await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block,null);
			List<CardModel> cardModel = CardFactory.GetDistinctForCombat(base.Owner.Creature.Player, from c in ModelDb.CardPool<TokenCardPool>().GetUnlockedCards(base.Owner.Creature.Player.UnlockState, base.Owner.Creature.Player.RunState.CardMultiplayerConstraint)
			    where c.Keywords.Contains(MyKeywords.Modification)
			    select c, 1, base.Owner.RunState.Rng.CombatCardGeneration).ToList();
		    if (cardModel != null)
		    {
		    	IReadOnlyList<CardPileAddResult> results = await CardPileCmd.AddGeneratedCardsToCombat(cardModel, PileType.Exhaust, base.Owner);
				CardCmd.PreviewCardPileAdd(results);
            }
		}
	}
}
