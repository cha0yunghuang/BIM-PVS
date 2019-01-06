Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Imports Com = Autodesk.Navisworks.Api.Interop.ComApi
Public Class Quaternion
    Private q As Double() = New Double(3) {}

    ' Construct from raw values.

    Public Sub New(a As Double, b As Double, c As Double, d As Double)
        q(0) = a
        q(1) = b
        q(2) = c
        q(3) = d
    End Sub
    ' Construct from a COM rotation object.

    Public Sub New(rot As Com.InwLRotation3f)
        Dim angle As Double = rot.angle
        Dim axis As Com.InwLUnitVec3f = rot.GetAxis()

        Dim s As Double = Math.Sin(angle / 2.0)
        q(0) = axis.data1 * s
        q(1) = axis.data2 * s
        q(2) = axis.data3 * s
        q(3) = Math.Cos(angle / 2.0)
    End Sub
    ' Construct from angle/axis pair.

    Public Shared Function FromAngleAxis(x As Double, y As Double, z As Double, angle As Double) As Quaternion
        Dim s As Double = Math.Sin(angle / 2.0)
        Return New Quaternion(x * s, y * s, z * s, Math.Cos(angle / 2.0))
    End Function
    ' Convert quaternion to a COM rotation object.

    Public Function ToRotation() As Com.InwLRotation3f
        Dim state As Com.InwOpState10 = Autodesk.Navisworks.Api.ComApi.ComApiBridge.State

        Dim rot As Com.InwLRotation3f = DirectCast(DirectCast(state.ObjectFactory(Com.nwEObjectType.eObjectType_nwLRotation3f), Object), Com.InwLRotation3f)
        Dim axis As Com.InwLUnitVec3f = DirectCast(DirectCast(state.ObjectFactory(Com.nwEObjectType.eObjectType_nwLUnitVec3f), Object), Com.InwLUnitVec3f)

        Dim s As Double = 1.0 / Length2
        axis.SetValue(q(0) * s, q(1) * s, q(2) * s)

        Dim angle As Double = 2.0 * Math.Acos(q(3))
        rot.SetValue(axis, angle)

        Return rot
    End Function
    ' Multiply two quaternions together.

    Public Shared Function Multiply(r1 As Quaternion, r2 As Quaternion) As Quaternion
        Dim res As New Quaternion(r2.q(3) * r1.q(0) + r2.q(0) * r1.q(3) + r2.q(1) * r1.q(2) - r2.q(2) * r1.q(1), r2.q(3) * r1.q(1) + r2.q(1) * r1.q(3) + r2.q(2) * r1.q(0) - r2.q(0) * r1.q(2), r2.q(3) * r1.q(2) + r2.q(2) * r1.q(3) + r2.q(0) * r1.q(1) - r2.q(1) * r1.q(0), r2.q(3) * r1.q(3) - r2.q(0) * r1.q(0) - r2.q(1) * r1.q(1) - r2.q(2) * r1.q(2))
        res.Normalise()
        Return res
    End Function
    ' Length of quaternion.

    Public ReadOnly Property Length() As Double
        Get
            Return Math.Sqrt(q(0) * q(0) + q(1) * q(1) + q(2) * q(2) + q(3) * q(3))
        End Get
    End Property
    ' Normalises the quaternion.

    Public Sub Normalise()
        Dim s As Double = 1.0 / Length
        q(0) *= s
        q(1) *= s
        q(2) *= s
        q(3) *= s
    End Sub
    ' "Length" of first three components of quaternion.

    Private ReadOnly Property Length2() As Double
        Get
            Return Math.Sqrt(q(0) * q(0) + q(1) * q(1) + q(2) * q(2))
        End Get
    End Property
End Class
