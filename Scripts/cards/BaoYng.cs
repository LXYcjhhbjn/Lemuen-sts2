using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace LXY.Scripts;

[Pool(typeof(lemuenCardPool))]
public class BaoYang: CustomCardModel
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<AMMO>(1m)];
    public override string PortraitPath => $"res://lemuen/images/cards/{Id.Entry.ToLowerInvariant()}.png";
	protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<AMMO>()];	
	public BaoYang() : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self , true)
	{
    }
	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
	{
		await PowerCmd.Apply<AMMO>(choiceContext , base.Owner.Creature,  base.DynamicVars["AMMO"].BaseValue, base.Owner.Creature, this);
		List<CardModel> list = PileType.Hand.GetPile(base.Owner).Cards.Where((CardModel c) => c.Keywords.Contains(MyKeywords.Modification)).ToList();
		foreach (CardModel item in list)
		{
			if (item.IsUpgradable) 
            {
                CardCmd.Upgrade(item);
            }
		}
	}
	protected override void OnUpgrade()
	{
		base.DynamicVars["AMMO"].UpgradeValueBy(1m);
	}
}
