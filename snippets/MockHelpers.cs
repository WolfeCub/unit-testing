namespace BSD.Metro.VRP.WebAPI.Tests.Helpers
{
    public static class MockHelpers
    {
        public static Mock<DbSet<T>> CreateMockSet<T>(IQueryable<T> data, bool isAsync=false)
            where T : class
        {
            var queryableData = data.AsQueryable();
            var mockSet = new Mock<DbSet<T>>();

            if (isAsync)
            {
                mockSet.As<IDbAsyncEnumerable<T>>().Setup(m => m.GetAsyncEnumerator()).Returns(new TestDbAsyncEnumerator<T>(queryableData.GetEnumerator()));
                mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(new TestDbAsyncQueryProvider<T>(queryableData.Provider));
            }
            else
            {
                mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryableData.Provider);
                mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(queryableData.GetEnumerator());
            }
            
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryableData.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryableData.ElementType);

            return mockSet;
        }

        public static IReturnsResult<TContext> ReturnsDbSet<TEntity, TContext>(this IReturns<TContext, DbSet<TEntity>> setup, IQueryable<TEntity> entities, bool isAsync=false)
            where TEntity : class
            where TContext : DbContext
        {
            var mockSet = CreateMockSet(entities, isAsync);
            return setup.Returns(mockSet.Object);
        }

        public static IQueryable<FORM> CreateTestForms()
        {
            var mockForms = new List<FORM>
            {
                new FORM
                {
                    ID = 2, STATUS_ID = 2, VEHICLE_TYPE = "Bus", LRV_CAR = "", LRV_LOCATION = "", BUS_LOCATION = "",
                    DIRECTION = "East", INCIDENT_CLASSIFICATION = "dafjdsklfjasdlfdsjlk", CASE_NUM = "890418390",
                    INCIDENT_DT = DateTime.Now, SUSPECT_DESC = "", SYNOPSIS = "", MEDIA_TYPE = "DVD",
                    MEDIA_QUANTITY = 1, AGENCY_NAME = "jfkldjfklasdfjsalk", OFFICER_NAME = "fdjaifodjiaofjsd",
                    OFFICER_DSN = "12343", CONTACT_EMAIL = "test@email.com", CONTACT_PHONE = "1234567890",
                    CREATE_DT = DateTime.Now,
                    FORM_STATUS = new FORM_STATUS
                    {
                        ID = 2,
                        STATUS = "In Progress"
                    }
                },
                new FORM
                {
                    ID = 21, STATUS_ID = 1, VEHICLE_TYPE = "Bus", LRV_CAR = "", LRV_LOCATION = "", BUS_LOCATION = "",
                    DIRECTION = "East", INCIDENT_CLASSIFICATION = "something happen", CASE_NUM = "890418390",
                    INCIDENT_DT = DateTime.Now, SUSPECT_DESC = "", SYNOPSIS = "i didn't see anything", MEDIA_TYPE = "DVD",
                    MEDIA_QUANTITY = 1, AGENCY_NAME = "this weird govt type agency", OFFICER_NAME = "some guy",
                    OFFICER_DSN = "12343", CONTACT_EMAIL = "test2@email.com", CONTACT_PHONE = "1234567890",
                    CREATE_DT = DateTime.Now,
                    FORM_STATUS = new FORM_STATUS
                    {
                        ID = 1,
                        STATUS = "New Submission"
                    }
                }
            }.AsQueryable();

            return mockForms;
        }

        public static IQueryable<FORM_STATUS> CreateFormStatus()
        {
            var formStatusData = new List<FORM_STATUS>
            {
                new FORM_STATUS {ID = 1, STATUS = "New Submission"},
                new FORM_STATUS {ID = 2, STATUS = "In Progress"},
                new FORM_STATUS {ID = 3, STATUS = "Requester Notified"},
                new FORM_STATUS {ID = 4, STATUS = "Request Complete"},
                new FORM_STATUS {ID = 5, STATUS = "Request Cancelled"}
            }.AsQueryable();

            return formStatusData;
        }

        public static IQueryable<FORM_AUDIT> CreateTestAudits()
        {
            var testAuditsData = new List<FORM_AUDIT>().AsQueryable();

            return testAuditsData;
        }

        public static FormModel CreateMockFormModel()
        {
            return new FormModel
            {
                StatusID = 3,
                Status = "Requester Notified",
                VehicleType = "Bus",
                LrvCar = null,
                LrvLocation = null,
                BusLocation = null,
                Direction = "East",
                IncidentClassification = "stubbed object",
                CaseNum = "890418390",
                IncidentDt = DateTime.Now,
                SuspectDesc = null,
                Synopsis = null,
                MediaType = "DVD",
                MediaQuantity = 1,
                AgencyName = "stubbed object",
                OfficerName = "stubbed object",
                OfficerDSN = "12343",
                ContactEmail = "stubbed@email.com",
                ContactPhone = "1234567890",
                CreateDt = DateTime.Now,
                UpdateUser = null,
                ReCaptchaResponse = null
            };
        }
    }
}