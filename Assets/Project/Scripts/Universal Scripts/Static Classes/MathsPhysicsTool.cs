using UnityEngine;

namespace Onion_AI
{
    public static class MathsPhysicsTool 
    {
        public static Vector3 CubicLerp(Vector3 a, Vector3 b, Vector3 c, Vector3 d, float interpolateAmount)
        {
            Vector3 ab_bc = QuadraticLerp(a, b, c, interpolateAmount);
            Vector3 bc_cd = QuadraticLerp(b, c, d, interpolateAmount);

            return Vector3.Lerp(ab_bc, bc_cd, interpolateAmount);
        }
        
        private static Vector3 QuadraticLerp(Vector3 a, Vector3 b, Vector3 c, float interpolateAmount)
        {
            Vector3 ab = Vector3.Lerp(a, b, interpolateAmount);
            Vector3 bc = Vector3.Lerp(b, c, interpolateAmount);

            return Vector3.Lerp(ab, bc, interpolateAmount);
        }
    }
}
