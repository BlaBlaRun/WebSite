// Generated by typings
// Source: scripts/TypeLite.Net4.d.ts
declare module BlaBlaRunProject.Domain.Concrete {
	interface Users {
		AspNetUserId: System.Guid;
		Id: number;
		UserName: string;
		Workouts: BlaBlaRunProject.Domain.Concrete.Workouts[];
	}
	interface Workouts {
		AVGPace: System.TimeSpan;
		Circular: boolean;
		City: string;
		Country: string;
		Distance: number;
		ElevationGain: number;
		EndLocation: System.Data.Entity.Spatial.DbGeography;
		Id: number;
		MaxNumberPeople: number;
		Region: string;
		StartDateTime: Date;
		StartLocation: System.Data.Entity.Spatial.DbGeography;
		Users: BlaBlaRunProject.Domain.Concrete.Users;
		UsersId: number;
		Zone: string;
	}
}
declare module Models.ViewModel {
	interface JSONReturnVM<T> {
		element: T;
		errormessage: string;
		haserror: boolean;
	}
}
declare module System {
	interface Guid {
	}
	interface TimeSpan {
		Days: number;
		Hours: number;
		Milliseconds: number;
		Minutes: number;
		Seconds: number;
		Ticks: number;
		TotalDays: number;
		TotalHours: number;
		TotalMilliseconds: number;
		TotalMinutes: number;
		TotalSeconds: number;
	}
}
declare module System.Data.Entity.Spatial {
	interface DbGeography {
		Area: number;
		CoordinateSystemId: number;
		DefaultCoordinateSystemId: number;
		Dimension: number;
		ElementCount: number;
		Elevation: number;
		EndPoint: System.Data.Entity.Spatial.DbGeography;
		IsClosed: boolean;
		IsEmpty: boolean;
		Latitude: number;
		Length: number;
		Longitude: number;
		Measure: number;
		PointCount: number;
		Provider: System.Data.Entity.Spatial.DbSpatialServices;
		ProviderValue: any;
		SpatialTypeName: string;
		StartPoint: System.Data.Entity.Spatial.DbGeography;
		WellKnownValue: System.Data.Entity.Spatial.DbGeographyWellKnownValue;
	}
	interface DbGeographyWellKnownValue {
		CoordinateSystemId: number;
		WellKnownBinary: number[];
		WellKnownText: string;
	}
	interface DbSpatialServices {
		Default: System.Data.Entity.Spatial.DbSpatialServices;
		NativeTypesAvailable: boolean;
	}
}