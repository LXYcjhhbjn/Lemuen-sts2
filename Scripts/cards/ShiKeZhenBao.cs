using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace LXY.Scripts;

[Pool(typeof(lemuenCardPool))]
public class ShiKeZhenBao: CustomCardModel
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<AMMO>(3m)];
    public override string PortraitPath => $"res://lemuen/images/cards/{Id.Entry.ToLowerInvariant()}.png";
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
		HoverTipFactory.FromPower<AMMO>(),
		HoverTipFactory.FromPower<ZhenBaoDan>(),
		HoverTipFactory.FromPower<ZhenChan>(),
		HoverTipFactory.FromPower<MaZuiDan>(),
		HoverTipFactory.FromCard<MaZuiZhuangTian>(),
		HoverTipFactory.FromCard<ZhenChanZhuangTian>()
    ];
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];
	public ShiKeZhenBao()
		: base(4, CardType.Skill, CardRarity.Rare, TargetType.Self,true)
	{
    }
	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
	{
		await PowerCmd.Apply<AMMO>(choiceContext,base.Owner.Creature, base.DynamicVars["AMMO"].BaseValue, base.Owner.Creature, this);
		List<CardModel>cards= new List<CardModel> {base.CombatState.CreateCard<MaZuiZhuangTian>(base.Owner),base.CombatState.CreateCard<ZhenChanZhuangTian>(base.Owner)};
		CardModel cardModel = await CardSelectCmd.FromChooseACardScreen(choiceContext, cards, base.Owner, canSkip: false);
		if (cardModel != null)
		{
			if (cardModel is MaZuiZhuangTian)
			{
				await PowerCmd.Apply<MaZuiDan>(choiceContext,base.Owner.Creature, 5, base.Owner.Creature, this);
			}
			else
			{
				await PowerCmd.Apply<ZhenBaoDan>(choiceContext,base.Owner.Creature, 3, base.Owner.Creature, this);
			}
		}
	}
	protected override void OnUpgrade()
	{
		base.EnergyCost.UpgradeBy(-1);
	}
}
