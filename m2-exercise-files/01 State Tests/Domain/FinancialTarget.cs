using Collections;

namespace Domain
{
    public class FinancialTarget
    {
        // Test case: count = 2 -> adds 3, 5 to the array
        public void AddTargetPoints(MyArray toArray, int count)
        {
            for (int i = 0; i < count; i++)
            {
                toArray.Append(3 + 2*i);
            }
        }

        public MyArray InitializePoints()
        {
            MyArray points = new MyArray();
            points.Append(3);
            return points;
        }
    }
}
