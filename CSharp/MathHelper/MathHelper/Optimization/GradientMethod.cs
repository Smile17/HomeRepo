using MathHelper.Algebra;
using MathHelper.Analysis;

namespace MathHelper.Optimization
{
    class GradientMethod
    {
        public static Vector GradPermanentStep(Function.Func F, Vector v0, double alpha, double eps)
        {
            Vector vk = new Vector(v0);
            int i = 0;
            Vector vk1 = new Vector(vk);
            do
            {
                vk1.Copy(vk);   //vk=vk1
                vk1 = vk - alpha * Function.Grad(vk, 0.01, F);
                i++;
            }
            while ((i < 1000) && ((vk1 - vk).Norm() > eps));
            return vk1;
        }

        public static Vector GradDivisionStep(Function.Func F, Vector v0, double alpha, double lamda, double eps)
        {
            Vector vk = new Vector(v0);
            int i = 0;
            Vector vk1 = new Vector(vk);
            do
            {
                vk1.Copy(vk);   //vk=vk1
                vk1 = vk - alpha * Function.Grad(vk, 0.01, F);
                while (F(vk1) - F(vk) > 0)
                {
                    alpha = alpha * lamda;
                    vk1 = vk - alpha * Function.Grad(vk, 0.01, F);
                }
                i++;
            }
            while ((i < 1000) && ((vk1 - vk).Norm() > eps));
            return vk1;
        }


    }
}
