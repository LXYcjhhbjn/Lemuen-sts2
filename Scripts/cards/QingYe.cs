using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace LXY.Scripts;

[Pool(typeof(lemuenCardPool))]
public class QingYe: CustomCardModel
{
    protected override bool ShouldGlowGoldInternal => IsPlayable;
	public override int MaxUpgradeLevel => 999;
    protected override bool IsPlayable => Owner.Creature.HasPower<AMMO>();
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Retain,MyKeywords.Guns];
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(8m, ValueProp.Move)];
    public override string PortraitPath => $"res://lemuen/images/cards/{Id.Entry.ToLowerInvariant()}.png";
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
		HoverTipFactory.FromPower<AMMO>()
    ];
    public QingYe() : base(0, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy,true)
    {
    }
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
		ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
		await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
		List<CardModel> list1 = PileType.Hand.GetPile(base.Owner).Cards.Where((CardModel c) => c != null && c.Type == CardType.Attack).ToList();
		int cardCount = list1.Count;
		foreach (CardModel item in list1)
		{
			await CardCmd.Exhaust(choiceContext, item);
		}
        if (cardPlay.Card.Owner.Creature.HasPower<AMMO>())
		{
		    await using AttackContext context = await AttackCommand.CreateContextAsync(base.CombatState, choiceContext,this);
		    List<DamageResult> list = (await CreatureCmd.Damage(choiceContext, cardPlay.Target, base.DynamicVars.Damage.BaseValue, ValueProp.Move, this)).ToList();
		    context.AddHit(list);
		    DamageResult damageResult = list.FirstOrDefault();
		    if (damageResult != null)
		    {
			    List<Creature> list2 = (from e in base.CombatState.GetTeammatesOf(damageResult.Receiver)where e.IsHittable select e).ToList();
			    if (list2.Count != 0)
		    	{
			    	AttackContext attackContext = context;
			    	attackContext.AddHit(await CreatureCmd.Damage(choiceContext, list2, (damageResult.TotalDamage + damageResult.OverkillDamage) >> 2, ValueProp.Unpowered | ValueProp.Move, base.Owner.Creature, this));
		    	}
		    }
            await PowerCmd.Apply<AMMO>(choiceContext , base.Owner.Creature, -1 , base.Owner.Creature, this);
        }
    }
	protected override PileType GetResultPileTypeForCardPlay()
	{
		PileType resultPileType = base.GetResultPileTypeForCardPlay();
		if (resultPileType != PileType.Discard)
		{
			return resultPileType;
		}
		return PileType.Hand;
	}
	protected override void OnUpgrade()
	{
		base.DynamicVars.Damage.UpgradeValueBy(3m);
		AddKeyword(CardKeyword.Innate);
	}
}