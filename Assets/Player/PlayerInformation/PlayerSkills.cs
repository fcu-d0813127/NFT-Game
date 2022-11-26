using System;

public class PlayerSkills {
  public static int[] CreateSkills(string skills) {
    return Array.ConvertAll(skills.Split(','), int.Parse);
  }
}
