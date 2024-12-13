
namespace CarRental.Common.Enums;

public enum FuelType : byte
{
    DIESEL = 1,
    PETROL = 2,
    ELECTRIC = 3,
    HYBRID = 4,
    LPG = 5
}


public enum TransmissionType : byte
{
    MANUAL = 1,
    AUTOMATIC = 2,
    SEMI_AUTOMATIC = 3,
    TRIPTRONIC = 4
}

public enum CarFeatureType : byte
{
    COLOR,
    ENGINE,
    NO_OF_DOORS,
    NO_OF_SEATS,
}

public enum MediaType : byte
{
    THUMBNAIL = 1,
    MAIN_SLIDER = 2
}
