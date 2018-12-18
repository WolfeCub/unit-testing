[TestClass]
public class ExControllerTests
{
    private Mock<DbSet<FORM>> _formsMockSet;
    private Mock<DbSet<FORM>> _formsMockSetAsync;
    private Mock<DbSet<FORM_STATUS>> _formStatusMockSet;
    private Mock<DbSet<FORM_STATUS>> _formStatusMockSetAsync;
    private Mock<DvrrEntities> _mockContext;
    private Mock<DbSet<FORM_AUDIT>> _formAuditsData;
    private Mock<DbSet<FORM_AUDIT>> _formAuditsDataAsync;
    private Mock<IMapper> _mapperMock;
    private Mock<IWcfEmail> _emailServiceMock;

    [TestInitialize]
    public void TestSetup()
    {
        _mapperMock = new Mock<IMapper>();
        _emailServiceMock = new Mock<IWcfEmail>();

        _emailServiceMock
            .Setup(m => m.SendHtmlEmailAsync(
              It.IsAny<string>(), 
              It.IsAny<string>(), 
              It.IsAny<string>(),
              It.IsAny<string>()))
            .Returns(Task.FromResult(0));
    }

    [TestCleanup]
    public void TestCleanup()
    {
        _formsMockSet = null;
        _formsMockSetAsync = null;
        _formStatusMockSet = null;
        _formStatusMockSetAsync = null;
        _formAuditsData = null;
        _formAuditsDataAsync = null;
        _mockContext = null;
    }

    [TestMethod]
    public void GetFORMsTest()
    {
        // ARRANGE
        var expected = new FormModel();
        _mapperMock.Setup(m => 
          m.Map<FORM, FormModel>(It.IsAny<FORM>())
        ).Returns(expected);

        _mockContext = new Mock<DvrrEntities>();
        _mockContext.Setup(m => m.FORMs)
          .ReturnsDbSet(MockHelpers.CreateTestForms());
        _mockContext.Setup(m => m.FORM_STATUS)
          .ReturnsDbSet(MockHelpers.CreateFormStatus());
        _mockContext.Setup(m => m.FORM_AUDIT)
          .ReturnsDbSet(MockHelpers.CreateTestAudits());

        // ACT
        var controller = new ExController(
          _mockContext.Object, 
          _mapperMock.Object, 
          _emailServiceMock.Object);
        controller.ShouldNotBeNull();

        var forms = controller.GetFORMs();

        // ASSERT
        _mockContext.Verify(v => v.FORMs);

        forms.ShouldNotBeNull();
        forms.Count().ShouldBe(2);
    }
}
