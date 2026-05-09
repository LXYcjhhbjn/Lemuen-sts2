using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace LXY.Scripts;

[Pool(typeof(lemuenCardPool))]
public class QiangZhiZhuiSi: CustomCardModel
{
    private const string _increaseKey = "Increase";
    private decimal _extraDamageFromPlays;
    protected override bool ShouldGlowGoldInternal => IsPlayable;
    protected override bool IsPlayable => Owner.Creature.HasPower<AMMO>();
	public override int MaxUpgradeLevel => 999;
    private const int energyCost = 0;
    private const CardType type = CardType.Attack;
    private const CardRarity rarity = CardRarity.Rare;
    private const TargetType targetType = TargetType.AllEnemies;
    private const bool shouldShowInCardLibrary = true;
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Retain,MyKeywords.Guns];
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(6m, ValueProp.Move),new DynamicVar("Increase", 3m)];
	private decimal ExtraDamageFromPlays
	{
		get
		{
			return _extraDamageFromPlays;
		}
		set
		{
			AssertMutable();
			_extraDamageFromPlays = value;
		}
	}
    public override string PortraitPath => $"res://lemuen/images/cards/{Id.Entry.ToLowerInvariant()}.png";
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
		HoverTipFactory.FromPower<AMMO>()
    ];
    public QiangZhiZhuiSi() : base(energyCost, type, rarity, targetType, shouldShowInCardLibrary)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
		List<CardModel> list = PileType.Hand.GetPile(base.Owner).Cards.Where((CardModel c) => c != null && c.Type == CardType.Attack).ToList();
		int cardCount = list.Count;
		foreach (CardModel item in list)
		{
			await CardCmd.Exhaust(choiceContext, item);
		}
        if (cardPlay.Card.Owner.Creature.HasPower<AMMO>())
		{
            await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this)
            .WithHitFx("vfx/vfx_attack_blunt", null, "blunt_attack.mp3")
			.TargetingAllOpponents(base.CombatState)
			.Execute(choiceContext);
		    base.DynamicVars.Damage.BaseValue += base.DynamicVars["Increase"].BaseValue;
		    ExtraDamageFromPlays += base.DynamicVars["Increase"].BaseValue;
            await PowerCmd.Apply<AMMO>(choiceContext,base.Owner.Creature, -1 , base.Owner.Creature, this);
		}
    }
	protected override void AfterDowngraded()
	{
		base.AfterDowngraded();
		base.DynamicVars.Damage.BaseValue += ExtraDamageFromPlays;
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
        DynamicVars.Damage.UpgradeValueBy(3m);
		AddKeyword(CardKeyword.Innate);
    }
}