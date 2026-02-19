using System;
using System.Collections.Generic;

namespace ContractQueen.Persist;

internal static class FrogDetailsManager
{

  private static readonly Random rng = new();

  private readonly static List<string> FrogPersonaCommon = [
    "Hoppy", "Chill", "Sleepy", "Grumpy", "Hungry", "Braggart", "Smart",
    "Tough", "Dizzy", "Meek", "Slob", "Lazy", "Cool", "Reserved",
    "Shy", "Quiet", "Jealous", "Relaxed", "Fit", "Follower", "Noisy",
    "Young", "Old", "Vegetarian", "Atheist", "Zealot", "Home Cook",
    "Mage", "Wizard", "Moist", "Round", "Goofy", "Judgemental",
    "Baker", "Clumsy", "Energetic", "Bouncy", "Workaholic", "Bookworm",
    "Polite", "Unbothered", "Sticky", "Slimy", "Proud Parent",
    "Confused", "Round", "Neighborly", "Damp", "Herbivore",
  ];
  private readonly static List<string> FrogPersonaUncommon = [
    "Yoga Instructor", "Athletic", "Obsessed", "Lucky", "Silver Medal",
    "Programmer", "Knitter", "Charismatic", "Cosplayer", "Loyal",
    "Streamer", "Polyglot", "Witch's Familiar", "Accountant",
    "Coffee Addict", "Tax Evader", "Podcaster", "Vinyl Collector",
    "Volunteer Firefighter", "Dancer", "Greased Lightning",
    "Hard Worker", "Sunny", "Helpful", "Grouch", "Bad Tipper",
    "Intern", "Lactose Intolerant", "Homeowner", "Barista",
    "Cross-fitter", "Washed Up", "Ex-Convict", "Designated Driver",
    "Influencer", "Reformed", "Banished", "Rebel", "Crowdfunded"
  ];
  private readonly static List<string> FrogPersonaRare = [
    "Royalty", "Head Chef", "Bodybuilder", "Martial Artist",
    "Secret Agent", "Golden", "Presidential", "Extraterrestrial",
    "Galaxy Brain", "Time Traveler", "Disguised", "Tasty?",
    "Prophet", "Cursed", "Sentient", "Ghost", "Investment Banker",
    "Peerless", "DLC Frog", "Non-Canon", "Low Resolution",
  ];

  private readonly static List<string> FrogPersonaLegendary =
  [
    "Lottery Winner", "Frog-Born", "Legendary", "Void Spawn",
    "Universal Constant", "Billionaire", "CEO Of Ponds", "A Toad",
    "Main Character", "Pre-Order Bonus", "Biblically Accurate",
    "Infinite", "Inter-Dimensional Entity", "Unstoppable Force",
    "Non-Fungible Toad", "Self Aware", "The ONE Frog"
  ];

  private readonly static List<string> FrogNames = [
    "Bartholomew", "Barnaby", "Winston", "Mortimer", "Archibald",
    "Clarence", "Eugene", "Cornelius", "Agatha", "Beatrice", "Gribbit",
    "Croak", "Burp", "Peep", "Gulp", "Kero", "Ribb", "Chirp", "Plop",
    "Wart", "Bob", "Grog", "Bub", "Pip", "Zog", "Mud", "Pudd", "Bog",
    "Jojo", "Gub", "Lily", "Tad", "Polly", "Jeremiah", "Kermit", "Tiana",
    "Michigan", "Bloop", "Ooz", "Zorp", "Xylophone", "Fergus", "Hopper",
    "Sprout", "Moss", "Cricket", "Slug", "Gumbo", "Pickle", "Dunk", "Skip",
    "Flippy", "Toadie", "Ser Hopsalot", "Lady Padd", "Bubbles", "Gunk",
    "Squelch", "Muck", "Algae", "Swampy", "Leaper", "Webbe", "Puddle",
    "Dew", "Misty", "Robyn", "Rose", "Darmuh", "Omni", "Frogbert", "Toadsworth",
    "Seanie", "Fay", "Gibbuns", "Gimlet", "Terry", "Poole", "Potato",
    "Lumpy", "Bingus", "Blooper", "Dozer", "Sodsie", "Manfred", "Broccoli",
    "Meatloaf", "Atreyu", "Pipsy", "Lat", "Mister", "Missus", "Leopold",
    "Raster", "Stinky", "Ranch Dressing", "Plipper", "Puppy", "Kitten",
    "Speedy", "Nitro", "Mecha Frogbert", "Melody", "Hilly", "Dorn",
    "Skrat", "Tumby", "Reseph", "Iono", "Nameless Wanderer", "Macbeth",
    "Skeeter", "Triton", "Solan", "Preti", "Slippage", "Rusty", "Ruby",
    "Greg", "Polonius", "Greebler", "Succotash", "Kretch", "Fabian",
    "Pompadour", "Matador", "Supreme", "Jambone", "Tilly", "X-Man",
    "Punk", "Duke", "Duchess", "Mario", "Pliskin", "Helena", "Akbal",
    "Nugget", "Scampi", "Pesto", "Zesty", "Gherkin", "Cabbage",
    "Cattail", "T-Bone", "Chad", "Kyle", "Karen", "Linda", "Puddles",
    "Frogger", "Battletoad", "Slippy", "Bullseye", "Hypno", "Newt",
    "Waffles", "Pancake", "Toast", "Oatmeal", "Crouton", "Tater",
    "Kevin", "Gary", "Steve", "Dave", "Susan", "Barb", "Spud",
    "Gumboil", "Wartsly", "Ribbiton", "Hopkins", "Croaker", "Xyler",
    "Benson", "Waverly", "Candy", "Poplar", "Lunch", "Crockett"
  ];

  static FrogDetailsManager()
  {

  }

  internal static (string Name, string Persona) GetCompleteFrogProfile()
  {
    var rarity = rng.NextDouble();
    string persona = "Mysterious";
    string name = "Stranger";

    if (rarity > 0.985d)
      FrogPersonaLegendary.GetRandom(ref persona);
    else if (rarity > 0.800d)
      FrogPersonaRare.GetRandom(ref persona);
    else if (rarity > 0.500d)
      FrogPersonaUncommon.GetRandom(ref persona);
    else
      FrogPersonaCommon.GetRandom(ref persona);

    FrogNames.GetRandom(ref name);

    return (name, persona);
  }

  public static void GetRandom<T>(this List<T> list, ref T value)
  {
    if (list == null || list.Count == 0) return;
    value = list[rng.Next(list.Count)];
  }
}
