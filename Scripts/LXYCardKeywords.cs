using BaseLib.Patches.Content;
using MegaCrit.Sts2.Core.Entities.Cards;

namespace LXY.Scripts;
public class MyKeywords
{
    [CustomEnum("MODIFICATION")]
    [KeywordProperties(AutoKeywordPosition.Before)]
    public static CardKeyword Modification;

    [CustomEnum("GUNS")]
    [KeywordProperties(AutoKeywordPosition.Before)]
    public static CardKeyword Guns;
}