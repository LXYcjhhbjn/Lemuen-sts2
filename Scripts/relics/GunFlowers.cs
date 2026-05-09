using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace LXY.Scripts;

[Pool(typeof(lemuenRelicPool))]
public class GunFlowers : CustomRelicModel
{
    public override RelicRarity Rarity => RelicRarity.Starter;
    protected override IEnumerable<DynamicVar> CanonicalVars => [new CardsVar(1)];
    // 小图标
    public override string PackedIconPath => $"res://lemuen/images/relics/GunFlowers.png";
    // 轮廓图标
    protected override string PackedIconOutlinePath => $"res://lemuen/images/relics/GunFlowers.png";
    // 大图标
    protected override string BigIconPath => $"res://lemuen/images/relics/GunFlowers.png";
	public override async Task AfterSideTurnStart(CombatSide side, ICombatState icombatState)
	{
		if (side == base.Owner.Creature.Side && icombatState.RoundNumber <= 1)
		{
			Flash();
			await PowerCmd.Apply<ZiYuanDiaoPeiPower>(new ThrowingPlayerChoiceContext() , base.Owner.Creature, 1, base.Owner.Creature, null);
			await PowerCmd.Apply<TongJi>(new ThrowingPlayerChoiceContext() , icombatState.HittableEnemies, 1, base.Owner.Creature, null);
		}
	}
	public override decimal ModifyHandDraw(Player player, decimal count)
	{
		if (player != base.Owner)
		{
			return count;
		}
		if (player.Creature.CombatState.RoundNumber > 1)
		{
			return count;
		}
		return count + base.DynamicVars.Cards.BaseValue;
	}
}