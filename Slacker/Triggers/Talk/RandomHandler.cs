using System;

namespace Slacker.Triggers.Talk
{
    public static class RandomHandler
    {
        private static Random _random;

        public static Random Random => _random ?? (_random = new Random());
    }
}
