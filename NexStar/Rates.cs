﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using ASCOM.DeviceInterface;
using System.Collections;

namespace ASCOM.NexStar
{
    #region Rate class
    //
    // The Rate class implements IRate, and is used to hold values
    // for AxisRates. You do not need to change this class.
    //
    // The Guid attribute sets the CLSID for ASCOM.NexStar.Rate
    // The ClassInterface/None addribute prevents an empty interface called
    // _Rate from being created and used as the [default] interface
    //
    [Guid("d2a027fc-4476-4de6-b641-7c8358b6343b")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComVisible(true)]
    public class Rate : ASCOM.DeviceInterface.IRate
    {
        private double maximum = 0;
        private double minimum = 0;

        //
        // Default constructor - Internal prevents public creation
        // of instances. These are values for AxisRates.
        //
        internal Rate(double minimum, double maximum)
        {
            this.maximum = maximum;
            this.minimum = minimum;
        }

        #region Implementation of IRate

        public void Dispose()
        {
            //throw new System.NotImplementedException();
        }

        public double Maximum
        {
            get { return this.maximum; }
            set { this.maximum = value; }
        }

        public double Minimum
        {
            get { return this.minimum; }
            set { this.minimum = value; }
        }

        #endregion
    }
    #endregion

    #region AxisRates
    //
    // AxisRates is a strongly-typed collection that must be enumerable by
    // both COM and .NET. The IAxisRates and IEnumerable interfaces provide
    // this polymorphism. 
    //
    // The Guid attribute sets the CLSID for ASCOM.NexStar.AxisRates
    // The ClassInterface/None addribute prevents an empty interface called
    // _AxisRates from being created and used as the [default] interface
    //
    [Guid("910a7b5a-f0cc-447b-8583-9e8769c2dec6")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComVisible(true)]
    public class AxisRates : IAxisRates, IEnumerable
    {
        private TelescopeAxes axis;
        private readonly Rate[] rates;

        //
        // Constructor - Internal prevents public creation
        // of instances. Returned by Telescope.AxisRates.
        //
        internal AxisRates(TelescopeAxes axis, params Rate[] axisRate)
        {
            this.axis = axis;
            //
            // This collection must hold zero or more Rate objects describing the 
            // rates of motion ranges for the Telescope.MoveAxis() method
            // that are supported by your driver. It is OK to leave this 
            // array empty, indicating that MoveAxis() is not supported.
            //
            // Note that we are constructing a rate array for the axis passed
            // to the constructor. Thus we switch() below, and each case should 
            // initialize the array for the rate for the selected axis.
            //
            switch (axis)
            {
                case TelescopeAxes.axisPrimary:
                    // TODO Initialize this array with any Primary axis rates that your driver may provide
                    // Example: m_Rates = new Rate[] { new Rate(10.5, 30.2), new Rate(54.0, 43.6) }
                    //this.rates = new Rate[0];
                    this.rates = axisRate;
                    break;
                case TelescopeAxes.axisSecondary:
                    // TODO Initialize this array with any Secondary axis rates that your driver may provide
                    //this.rates = new Rate[0];
                    this.rates = axisRate;
                    break;
                case TelescopeAxes.axisTertiary:
                    // TODO Initialize this array with any Tertiary axis rates that your driver may provide
                    //this.rates = new Rate[0];
                    this.rates = axisRate;
                    break;
            }
        }

        #region IAxisRates Members

        public int Count
        {
            get { return this.rates.Length; }
        }

        public void Dispose()
        {
            //throw new System.NotImplementedException();
        }

        public IEnumerator GetEnumerator()
        {
            return this.rates.GetEnumerator();
        }

        public IRate this[int index]
        {
            get { return this.rates[index - 1]; }	// 1-based
        }

        #endregion
    }
    #endregion

    #region TrackingRates
    //
    // TrackingRates is a strongly-typed collection that must be enumerable by
    // both COM and .NET. The ITrackingRates and IEnumerable interfaces provide
    // this polymorphism. 
    //
    // The Guid attribute sets the CLSID for ASCOM.NexStar.TrackingRates
    // The ClassInterface/None addribute prevents an empty interface called
    // _TrackingRates from being created and used as the [default] interface
    //
    [Guid("e059f6cf-e76f-4c24-9b8f-f31a4068177e")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComVisible(true)]
    public class TrackingRates : ITrackingRates, IEnumerable
    {
        private readonly DriveRates[] trackingRates;
        //
        // Default constructor - Internal prevents public creation
        // of instances. Returned by Telescope.AxisRates.
        //
        internal TrackingRates(params DriveRates[] Rates)
        {
            //
            // This array must hold ONE or more DriveRates values, indicating
            // the tracking rates supported by your telescope. The one value
            // (tracking rate) that MUST be supported is driveSidereal!
            //
            this.trackingRates = new DriveRates[Rates.Length + 1];
            this.trackingRates[0] = DriveRates.driveSidereal;
            Rates.CopyTo(this.trackingRates, 1);
        }

        #region ITrackingRates Members

        public int Count
        {
            get { return this.trackingRates.Length; }
        }

        public IEnumerator GetEnumerator()
        {
            return this.trackingRates.GetEnumerator();
        }

        public void Dispose()
        {
            //throw new System.NotImplementedException();
        }

        public DriveRates this[int index]
        {
            get { return this.trackingRates[index - 1]; }	// 1-based
        }

        #endregion
    }
    #endregion
}
