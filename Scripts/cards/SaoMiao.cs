using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace LXY.Scripts;

[Pool(typeof(lemuenCardPool))]
public class SaoMiao: CustomCardModel
{
    protected override bool ShouldGlowGoldInternal => IsPlayable;
    protected override bool IsPlayable => Owner.Creature.HasPower<AMMO>();
    public override IEnumerable<CardKeyword> CanonicalKeywords => [MyKeywords.Guns];
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<TongJi>(1m)];
    public override string PortraitPath => $"res://lemuen/images/cards/{Id.Entry.ToLowerInvariant()}.png";
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
		HoverTipFactory.FromPower<AMMO>(),
        HoverTipFactory.FromPower<TongJi>()
    ];
    public SaoMiao() : base(0, CardType.Skill, CardRarity.Uncommon, TargetType.Self,true)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (cardPlay.Card.Owner.Creature.HasPower<AMMO>())
		{
			await PowerCmd.Apply<AMMO>(choiceContext , base.Owner.Creature, -1 , base.Owner.Creature, this);
            if (CombatState.HittableEnemies != null)
            {
                await PowerCmd.Apply<TongJi>(choiceContext,CombatState.HittableEnemies, base.DynamicVars["TongJi"].BaseValue , base.Owner.Creature, null);
            }
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
        base.DynamicVars["TongJi"].UpgradeValueBy(1m);
    }
}