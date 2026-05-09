using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Saves.Runs;

namespace LXY.Scripts;

[Pool(typeof(lemuenCardPool))]
public class KuaJingZhuiJiXuKe: CustomCardModel
{
	public override string PortraitPath => $"res://lemuen/images/cards/{Id.Entry.ToLowerInvariant()}.png";
    private const string _increaseKey = "Increase";
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromPower<TongJi>()
	];
	private int _currentTongJi = 1;

	private int _increasedTongJi;

	[SavedProperty]
	public int CurrentTongJi
	{
		get
		{
			return _currentTongJi;
		}
		set
		{
			AssertMutable();
			_currentTongJi = value;
			base.DynamicVars["TongJi"].BaseValue = _currentTongJi;
		}
	}

	protected override IEnumerable<DynamicVar> CanonicalVars => [new MaxHpVar(1m),new PowerVar<TongJi>(1m),new IntVar("Increase", 1m)];

    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Innate,CardKeyword.Exhaust];

	[SavedProperty]
	public int IncreasedTongJi
	{
		get
		{
			return _increasedTongJi;
		}
		set
		{
			AssertMutable();
			_increasedTongJi = value;
		}
	}

	public KuaJingZhuiJiXuKe()
		: base(2, CardType.Skill, CardRarity.Rare, TargetType.AllEnemies,true)
	{
	}

	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
	{
		await CreatureCmd.LoseMaxHp(choiceContext, base.Owner.Creature, base.DynamicVars.MaxHp.BaseValue, isFromCard: true);
		await PowerCmd.Apply<TongJi>(choiceContext , base.CombatState.HittableEnemies, base.DynamicVars["TongJi"].BaseValue, base.Owner.Creature, this);
		int intValue = base.DynamicVars["Increase"].IntValue;
		BuffFromPlay(intValue);
		(base.DeckVersion as KuaJingZhuiJiXuKe)?.BuffFromPlay(intValue);
	}

	protected override void OnUpgrade()
	{
		base.EnergyCost.UpgradeBy(-1);
	}

	protected override void AfterDowngraded()
	{
		UpdateTongJi();
	}

	private void BuffFromPlay(int extraTongJi)
	{
		IncreasedTongJi += extraTongJi;
		UpdateTongJi();
	}

	private void UpdateTongJi()
	{
		CurrentTongJi = 1 + IncreasedTongJi;
	}
}