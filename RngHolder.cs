using System;

namespace Sorting {
    internal static class RngHolder {
        private static Random Rand = null;

        public static Random GetRng() {
            if (RngHolder.Rand is null) {
                RngHolder.Rand = new Random();
            }

            return RngHolder.Rand;
        }
    }
}
