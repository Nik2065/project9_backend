namespace Logic.Tests
{
    public partial class Tests
    {
        [SetUp]
        public void Setup()
        {
            Console.WriteLine("���������� � ������������");

            //���� ������ ����������� �� �������, �� ���� ��������� ������ � ������
            _testdb = new DataAccess.DataContext();
        }

        public DataAccess.DataContext _testdb { get; set; }


        [Test]
        public void TestIfDbExists()
        {
            var plist = _testdb.Products.ToList();
        }

        /// <summary>
        /// ������������ ������ �� ����������
        /// </summary>
        [Test]
        public void TestSearchProducts()
        {
            var plist = _testdb.Products.ToList();
        }

    }
}