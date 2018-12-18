public class AutoMapperModule : Module {
    protected override void Load (ContainerBuilder builder) {
        builder.Register (c => 
            new MapperConfiguration (cfg => { 
                cfg.AddProfile<MappingProfile> (); 
            }))
            .SingleInstance ()
            .AutoActivate ()
            .AsSelf ();

        builder.Register (c => 
            c.Resolve<MapperConfiguration>()
            .CreateMapper(c.Resolve))
            .As<IMapper>()
            .InstancePerLifetimeScope ();
    }
}

public class MappingProfile : Profile {
    public MappingProfile () {
        CreateMap<FORM, FormModel> ().AfterMap ((form, model) => {
            model.AgencyName = form.AGENCY_NAME;
            model.BusLocation = form.BUS_LOCATION;
            model.CaseNum = form.CASE_NUM;
            model.ContactEmail = form.CONTACT_EMAIL;
            model.ContactPhone = form.CONTACT_PHONE;
            model.Direction = form.DIRECTION;
            model.ID = (int) form.ID;
            model.IncidentClassification = form.INCIDENT_CLASSIFICATION;
            model.IncidentDt = form.INCIDENT_DT;
            model.LrvCar = form.LRV_CAR;
            model.LrvLocation = form.LRV_LOCATION;
            model.MediaQuantity = (int) form.MEDIA_QUANTITY;
            model.MediaType = form.MEDIA_TYPE;
            model.OfficerName = form.OFFICER_NAME;
            model.OfficerDSN = form.OFFICER_DSN;
            model.StatusID = (int) form.STATUS_ID;
            model.SuspectDesc = form.SUSPECT_DESC;
            model.Synopsis = form.SYNOPSIS;
            model.VehicleType = form.VEHICLE_TYPE;
            model.Status = form.FORM_STATUS.STATUS;
            if (form.CREATE_DT != null) 
            {
                model.CreateDt = (DateTime) form.CREATE_DT;
            }
        });

        CreateMap<FormModel, FORM> ().AfterMap ((model, form) => {
                form.AGENCY_NAME = model.AgencyName;
                form.BUS_LOCATION = model.BusLocation;
                form.CASE_NUM = model.CaseNum;
                form.CONTACT_EMAIL = model.ContactEmail;
                form.CONTACT_PHONE = model.ContactPhone;
                form.DIRECTION = model.Direction;
                form.ID = model.ID;
                form.INCIDENT_CLASSIFICATION = model.IncidentClassification;
                form.INCIDENT_DT = model.IncidentDt;
                form.LRV_CAR = model.LrvCar;
                form.LRV_LOCATION = model.LrvLocation;
                form.MEDIA_QUANTITY = model.MediaQuantity;
                form.MEDIA_TYPE = model.MediaType;
                form.OFFICER_NAME = model.OfficerName;
                form.OFFICER_DSN = model.OfficerDSN;
                form.STATUS_ID = model.StatusID;
                form.SUSPECT_DESC = model.SuspectDesc;
                form.SYNOPSIS = model.Synopsis;
                form.VEHICLE_TYPE = model.VehicleType;
                form.CREATE_DT = model.CreateDt;
            })
            .ForMember (f => f.FORM_AUDIT, opt => opt.Ignore ())
            .ForMember (f => f.FORM_STATUS, opt => opt.Ignore ());

        CreateMap<FORM_STATUS, StatusModel> ();
        CreateMap<StatusModel, FORM_STATUS> ();
    }
}