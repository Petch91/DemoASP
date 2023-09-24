﻿namespace DemoASP.Models.Enum
{
   [Flags]
   public enum Role
   {
      None = 0,
      User = 1,
      Moderator = 2,
      Admin = 4,
   }
}
