using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace LXY.Scripts;

[Pool(typeof(lemuenCardPool))]
public class SheSha: CustomCardModel
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<TongJi>(1m),new DamageVar(0m, ValueProp.Unblockable | ValueProp.Unpowered | ValueProp.Move)];
    public override string PortraitPath => $"res://lemuen/images/cards/{Id.Entry.ToLowerInvariant()}.png";
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromPower<TongJi>()
    ];
    public SheSha() : base(1, CardType.Skill, CardRarity.Rare, TargetType.AllEnemies,true)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (base.CombatState.HittableEnemies != null)
        {
            foreach (Creature hittableEnemy in base.CombatState.HittableEnemies)
		    {
			    await PowerCmd.Apply<TongJi>(choiceContext , hittableEnemy , base.DynamicVars["TongJi"].BaseValue , base.Owner.Creature, this);
                base.DynamicVars.Damage.BaseValue=hittableEnemy.GetPowerAmount<TongJi>();
                await CreatureCmd.Damage(choiceContext, hittableEnemy, base.DynamicVars.Damage , this);
                await CreatureCmd.LoseMaxHp(choiceContext, hittableEnemy , 2 * base.DynamicVars.Damage.BaseValue  , isFromCard: true);
		    }
        }
    }
    protected override void OnUpgrade()
    {
        base.DynamicVars["TongJi"].UpgradeValueBy(1m);
    }
}