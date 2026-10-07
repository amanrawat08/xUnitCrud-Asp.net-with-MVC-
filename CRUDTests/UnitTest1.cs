namespace CRUDTests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        //Arrange 
        MyMath mm = new MyMath();
        int input1 = 10, input2 = 5;
        int expectedVal = 15;
        //Act
        int result = mm.Add(input1,input2);
        //Assert
        Assert.Equal(expectedVal , result);
    }
}
