using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Saves.Runs;

namespace LXY.Scripts;

[Pool(typeof(lemuenCardPool))]
public class ZhenCha: CustomCardModel
{
	public override string PortraitPath => $"res://lemuen/images/cards/{Id.Entry.ToLowerInvariant()}.png";
    private const string _increaseKey = "Increase";

	private int _currentAim = 1;

	private int _increasedAim;

	[SavedProperty]
	public int CurrentAim
	{
		get
		{
			return _currentAim;
		}
		set
		{
			AssertMutable();
			_currentAim = value;
			base.DynamicVars["AIM"].BaseValue = _currentAim;
		}
	}

	protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<AIM>(1m),new IntVar("Increase", 2m)];

    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

	[SavedProperty]
	public int IncreasedAim
	{
		get
		{
			return _increasedAim;
		}
		set
		{
			AssertMutable();
			_increasedAim = value;
		}
	}

	public ZhenCha()
		: base(1, CardType.Skill, CardRarity.Rare, TargetType.Self)
	{
	}

	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
	{
		await PowerCmd.Apply<AIM>(choiceContext,base.Owner.Creature, base.DynamicVars["AIM"].BaseValue, base.Owner.Creature, this);
		int intValue = base.DynamicVars["Increase"].IntValue;
		BuffFromPlay(intValue);
		(base.DeckVersion as ZhenCha)?.BuffFromPlay(intValue);
	}

	protected override void OnUpgrade()
	{
		base.DynamicVars["Increase"].UpgradeValueBy(1m);
	}

	protected override void AfterDowngraded()
	{
		UpdateAim();
	}

	private void BuffFromPlay(int extraAim)
	{
		IncreasedAim += extraAim;
		UpdateAim();
	}

	private void UpdateAim()
	{
		CurrentAim = 1 + IncreasedAim;
	}
}