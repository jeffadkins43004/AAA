Public Class LatLonRequest
    '
    ' Summary:
    '     Gets the altitude of the System.Device.Location.GeoCoordinate, in meters.
    '
    ' Returns:
    '     The altitude, in meters.

    Public Property Altitude As Double
    '
    ' Summary:
    '     Gets or sets the heading in degrees, relative to true north.
    '
    ' Returns:
    '     The heading in degrees, relative to true north.
    '
    ' Exceptions:
    '   T:System.ArgumentOutOfRangeException:
    '     System.Device.Location.GeoCoordinate.Course is set outside the valid range.
    Public Property Course As Double
    '
    ' Summary:
    '     Gets or sets the accuracy of the latitude and longitude that is given by the
    '     System.Device.Location.GeoCoordinate, in meters.
    '
    ' Returns:
    '     The accuracy of the latitude and longitude, in meters.
    '
    ' Exceptions:
    '   T:System.ArgumentOutOfRangeException:
    '     System.Device.Location.GeoCoordinate.HorizontalAccuracy is set outside the valid
    '     range.
    Public Property HorizontalAccuracy As Double
    '
    ' Summary:
    '     Gets a value that indicates whether the System.Device.Location.GeoCoordinate
    '     does not contain latitude or longitude data.
    '
    ' Returns:
    '     true if the System.Device.Location.GeoCoordinate does not contain latitude or
    '     longitude data; otherwise, false.
    Public ReadOnly Property IsUnknown As Boolean
    '
    ' Summary:
    '     Gets or sets the latitude of the System.Device.Location.GeoCoordinate.
    '
    ' Returns:
    '     Latitude of the location.
    '
    ' Exceptions:
    '   T:System.ArgumentOutOfRangeException:
    '     System.Device.Location.GeoCoordinate.Latitude is set outside the valid range.
    Public Property Latitude As Double
    '
    ' Summary:
    '     Gets or sets the longitude of the System.Device.Location.GeoCoordinate.
    '
    ' Returns:
    '     The longitude.
    '
    ' Exceptions:
    '   T:System.ArgumentOutOfRangeException:
    '     System.Device.Location.GeoCoordinate.Longitude is set outside the valid range.
    Public Property Longitude As Double
    '
    ' Summary:
    '     Gets or sets the speed in meters per second.
    '
    ' Returns:
    '     The speed in meters per second. The speed must be greater than or equal to zero,
    '     or System.Double.NaN.
    '
    ' Exceptions:
    '   System.ArgumentOutOfRangeException:
    '     System.Device.Location.GeoCoordinate.Speed is set outside the valid range.
    Public Property Speed As Double
    '
    ' Summary:
    '     Gets or sets the accuracy of the altitude given by the System.Device.Location.GeoCoordinate,
    '     in meters.
    '
    ' Returns:
    '     The accuracy of the altitude, in meters.
    '
    ' Exceptions:
    '   T:System.ArgumentOutOfRangeException:
    '     System.Device.Location.GeoCoordinate.VerticalAccuracy is set outside the valid
    '     range.
    Public Property VerticalAccuracy As Double


End Class

