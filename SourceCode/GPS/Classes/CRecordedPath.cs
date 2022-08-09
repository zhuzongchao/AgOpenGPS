using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;

namespace AgOpenGPS
{
    public class CRecPathPt
    {
        public double easting { get; set; }
        public double northing { get; set; }
        public double heading { get; set; }
        public double speed { get; set; }
        public bool autoBtnState { get; set; }

        //constructor
        public CRecPathPt(double _easting, double _northing, double _heading, double _speed,
                            bool _autoBtnState)
        {
            easting = _easting;
            northing = _northing;
            heading = _heading;
            speed = _speed;
            autoBtnState = _autoBtnState;
        }
    }

    public class CRecordedPath
    {
        //constructor
        public CRecordedPath(FormGPS _f)
        {
            mf = _f;
        }

        //pointers to mainform controls
        private readonly FormGPS mf;

        //the recorded path from driving around
        public List<CRecPathPt> recList = new List<CRecPathPt>();
        public List<CRecPathPt> driveList = new List<CRecPathPt>();

        //the dubins path to get there
        public List<CRecPathPt> shuttleDubinsList = new List<CRecPathPt>();

        public int shuttleListCount;

        //list of vec3 points of Dubins shortest path between 2 points - To be converted to RecPt
        public List<vec3> shortestDubinsList = new List<vec3>();

        //generated reference line
        public vec2 refPoint1 = new vec2(1, 1), refPoint2 = new vec2(2, 2);

        public double distanceFromRefLine, distanceFromCurrentLinePivot;
        private int A, B, C;

        public int currentPositonIndex;
        public int lookPositonIndex;


        //pure pursuit values
        public vec3 pivotAxlePosRP = new vec3(0, 0, 0);

        public vec3 homePos = new vec3();
        public vec2 goalPointRP = new vec2(0, 0);
        public double steerAngleRP, rEastRP, rNorthRP, ppRadiusRP;
        public vec2 radiusPointRP = new vec2(0, 0);

        public bool isBtnFollowOn, isEndOfTheRecLine, isRecordOn;
        public bool isDrivingRecordedPath, isFollowingDubinsToPath, isFollowingRecPath, isFollowingDubinsHome;

        public double pivotDistanceError, pivotDistanceErrorLast, pivotDerivative, pivotDerivativeSmoothed;
        //derivative counters
        private int counter2;
        public int lastA = 0;
        public double inty;
        public double steerAngleSmoothed, pivotErrorTotal;
        public double distSteerError, lastDistSteerError, derivativeDistError;

        public int resumeState;

        int starPathIndx = 0;
        public bool StartDrivingRecordedPath()
        {
            //create the dubins path based on start and goal to start of recorded path
            A = B = C = 0;

            if (recList.Count < 5) return false;

            //save a copy of where we started.
            homePos = mf.pivotAxlePos;

            // Try to find the nearest point of the recordet path in relation to the current position:
            double distance = double.MaxValue;
            int idx = 0;
            int i = 0;

            if (resumeState == 0) //start at the start
            {
                currentPositonIndex = 0;
                lookPositonIndex = 0;
                lastA = 0;
                idx = 0;
                starPathIndx = 0;   
            }

            else if (resumeState == 1) //resume from where stopped mid path
            {
                if (currentPositonIndex+5 > recList.Count )
                {
                    currentPositonIndex = 0;
                    lookPositonIndex = 0;
                    lastA = 0;
                    idx = 0;
                    starPathIndx = 0;
                }
                else
                {
                    idx = starPathIndx = currentPositonIndex=0;
                }
            }

            else //find closest point
            {
                foreach (CRecPathPt pt in recList)
                {
                    double temp = ((pt.easting - homePos.easting) * (pt.easting - homePos.easting))
                        + ((pt.northing - homePos.northing) * (pt.northing - homePos.northing));

                    if (temp < distance)
                    {
                        distance = temp;
                        idx = i;
                    }
                    i++;
                }

                //scootch down the line a bit
                if (idx + 5 < recList.Count)  idx += 5;
                else idx = recList.Count-1;

                starPathIndx = currentPositonIndex = idx=0;
                lastA = 0;
            }

            //the goal is the first point of path, the start is the current position
            vec3 goal = new vec3(recList[idx].easting, recList[idx].northing, recList[idx].heading);            
             
            //get the dubins for approach to recorded path
            GetDubinsPath(goal);
            shuttleListCount = shuttleDubinsList.Count;

            //has a valid dubins path been created?
            if (shuttleListCount == 0) return false;

            //starPathIndx = idx;
            
            //technically all good if we get here so set all the flags
            isFollowingDubinsHome = false;
            isFollowingRecPath = true;
            isFollowingDubinsToPath = false;
            isEndOfTheRecLine = false;
            //currentPositonIndex = 0;
            isDrivingRecordedPath = true;
            return true;
        }

        public bool trig;
        public double north;
        public int pathCount = 0;

        public void UpdatePosition()
        {
            //if (isFollowingDubinsToPath)
            //{
            //    //set a speed of 10 kmh
            //    mf.sim.stepDistance = shuttleDubinsList[C].speed / 50;

            //    pivotAxlePosRP = mf.pivotAxlePos;

            //    //StanleyDubinsPath(shuttleListCount);
            //    PurePursuitDubins(shuttleListCount);

            //    //check if close to recorded path
            //    int cnt = shuttleDubinsList.Count;
            //    pathCount = cnt - B;
            //    if (pathCount < 8)
            //    {
            //        double distSqr = glm.DistanceSquared(pivotAxlePosRP.northing, pivotAxlePosRP.easting, recList[starPathIndx].northing, recList[starPathIndx].easting);
            //        if (distSqr < 2)
            //        {
            //            isFollowingRecPath = true;
            //            isFollowingDubinsToPath = false;
            //            shuttleDubinsList.Clear();
            //            shortestDubinsList.Clear();
            //            C = starPathIndx;
            //            A = C + 3;
            //            B = A + 1;
            //        }
            //    }
            //}

            if (isFollowingRecPath)
            {
                pivotAxlePosRP = mf.pivotAxlePos;

                //StanleyRecPath(recListCount);
                PurePursuitRecPath(driveList.Count, driveList);

                //if end of the line then stop
                if (!isEndOfTheRecLine)
                {
                    mf.sim.stepDistance = driveList[C].speed / 34.86;
                    north = driveList[C].northing;

                    pathCount = driveList.Count - C;

                    //section control - only if different click the button
                    bool autoBtn = (mf.autoBtnState == FormGPS.btnStates.Auto);
                    trig = autoBtn;
                    if (autoBtn != driveList[C].autoBtnState) mf.btnSectionOffAutoOn.PerformClick();
                }
                else
                {
                    StopDrivingRecordedPath();
                    return;

                    //create the dubins path based on start and goal to start trip home
                    //GetDubinsPath(homePos);
                    //shuttleListCount = shuttleDubinsList.Count;

                    ////its too small
                    //if (shuttleListCount < 3)
                    //{
                    //    StopDrivingRecordedPath();
                    //    return;
                    //}

                    ////set all the flags
                    //isFollowingDubinsHome = true;
                    //A = B = C = 0;
                    //isFollowingRecPath = false;
                    //isFollowingDubinsToPath = false;
                    //isEndOfTheRecLine = false;
                }
            }

            if (isFollowingDubinsHome)
            {
                int cnt = shuttleDubinsList.Count;
                pathCount = cnt - B;
                if (pathCount < 3)
                {
                    StopDrivingRecordedPath();
                    return;
                }

                mf.sim.stepDistance = shuttleDubinsList[C].speed / 35;
                pivotAxlePosRP = mf.pivotAxlePos;

                //StanleyDubinsPath(shuttleListCount);
                PurePursuitDubins(shuttleListCount);
            }
        }

        public void StopDrivingRecordedPath()
        {
            isFollowingDubinsHome = false;
            isFollowingRecPath = false;
            isFollowingDubinsToPath = false;
            shuttleDubinsList.Clear();
            shortestDubinsList.Clear();
            mf.sim.stepDistance = 0;
            isDrivingRecordedPath = false;
            mf.btnPathGoStop.Image = Properties.Resources.boundaryPlay;
            mf.btnPathRecordStop.Enabled = true;
            mf.btnPickPath.Enabled = true;
            mf.btnResumePath.Enabled = true;
            driveList.Clear();
        }

        private void GetDubinsPath(vec3 goal)
        {
            CDubins.turningRadius = mf.vehicle.minTurningRadius * 1.2;
            int fac = 0;
            switch (mf.vehicle.turnCase)
            {
                case 0:
                    CDubins.turningRadius = Math.Max(mf.vehicle.minTurningRadius + (mf.vehicle.trackWidth / 2), (mf.vehicle.wheelbase * 1.0 / (Math.Tan(glm.toRadians(mf.vehicle.maxSteerAngle)))) + (0));
                                        
                    CDubins.turningRadius = Math.Max(CDubins.turningRadius, (mf.tool.halfToolWidth - mf.tool.toolOverlap + mf.tool.toolOffset * fac)) + .01;
                    break;
                case 1:
                    CDubins.turningRadius = (mf.vehicle.minTurningRadius + (mf.vehicle.trackWidth / 2));
                    break;
                case 2:
                    CDubins.turningRadius = ((mf.vehicle.wheelbase * 1.0 / (Math.Tan(glm.toRadians(mf.vehicle.maxSteerAngle)))) + (0));
                    break;
                case 3:
                    CDubins.turningRadius = (mf.tool.halfToolWidth - mf.tool.toolOverlap + mf.tool.toolOffset * fac) + .1;
                    break;

            }
            CDubins dubPath = new CDubins();

            // current psition
            pivotAxlePosRP = mf.pivotAxlePos;

            //bump it forward
            vec3 pt2 = new vec3
            {
                easting = pivotAxlePosRP.easting + (Math.Sin(pivotAxlePosRP.heading) * 3),
                northing = pivotAxlePosRP.northing + (Math.Cos(pivotAxlePosRP.heading) * 3),
                heading = pivotAxlePosRP.heading
            };

            //get the dubins path vec3 point coordinates of turn
            shortestDubinsList.Clear();
            shuttleDubinsList.Clear();
           // CDubins.driveDistance = .1;

            shortestDubinsList = dubPath.GenerateDubins(pt2, goal);

            //if Dubins returns 0 elements, there is an unavoidable blockage in the way.
            if (shortestDubinsList.Count > 0)
            {
                shortestDubinsList.Insert(0, mf.pivotAxlePos);

                //transfer point list to recPath class point style
                double temp = recList[5].speed;
                for (int i = 0; i < shortestDubinsList.Count; i++)
                {
                    CRecPathPt pt = new CRecPathPt(shortestDubinsList[i].easting, shortestDubinsList[i].northing, shortestDubinsList[i].heading, temp, false);
                    shuttleDubinsList.Add(pt);
                }
                GetDrivePath();
                return;
               
            }
        }

        private void GetDrivePath()
        {
            driveList.Clear();
            double spacing = Math.Min(mf.tool.toolWidth / 2, mf.vehicle.wheelbase / 2); ;
            double distance = 0;
            for(int i =0;i< shuttleDubinsList.Count;i++)
            {
                driveList.Add(shuttleDubinsList[i]);
            }
            for (int i = 0; i < recList.Count; i++)
            {
                driveList.Add(recList[i]);
            }

            int cnt = driveList.Count;
            

            //for (int i = 0; i < cnt; i++)
            //{
            //    int j = i + 1;

            //    if (j == cnt) j = 0;
            //    distance = glm.Distance(driveList[i].easting, driveList[i].northing, driveList[j].easting, driveList[j].northing);
            //    if (distance > spacing * 1)
            //    {

            //        vec3 pointB = new vec3((driveList[i].easting + driveList[j].easting) / 2.0,
            //            (driveList[i].northing + driveList[j].northing) / 2.0, driveList[i].heading);

            //        CRecPathPt pt = new CRecPathPt(pointB.easting, pointB.northing, pointB.heading, driveList[i].speed, driveList[i].autoBtnState);


            //        driveList.Insert(j, pt);
            //        cnt = driveList.Count;
            //        i--;
            //    }
            //}

            cnt = driveList.Count;
            
            for (int k = 0; k < 5; k++)
            {
                double maxdist = double.NegativeInfinity;
                for (int i = 2; i < cnt-1; i++)
                {
                    int j = i + 1;

                    
                    distance = glm.Distance(driveList[i].easting, driveList[i].northing, driveList[j].easting, driveList[j].northing);
                    double deltaHeading = Math.Abs(driveList[j].heading - driveList[i].heading);
                    
                    if (distance > spacing && deltaHeading > .03)
                    {
                        if (distance > maxdist) maxdist = distance;

                        vec3 pointB = new vec3((driveList[j].easting + driveList[i].easting) / 2.0,
                            (driveList[j].northing + driveList[i].northing) / 2.0, driveList[i].heading);

                        CRecPathPt pt = new CRecPathPt(pointB.easting, pointB.northing, pointB.heading, driveList[i].speed, driveList[i].autoBtnState);


                        driveList.Insert(j, pt);
                        cnt = driveList.Count;
                        i--;
                        
                    }
                }
               
                //if (maxdist < spacing) break;
            }

            for (int k = 0; k < 20; k++)
            {
                double mindist = double.PositiveInfinity;
                for (int i = 2; i < cnt - 1; i++)
                {
                    int j = i + 1;


                    distance = glm.Distance(driveList[i].easting, driveList[i].northing, driveList[j].easting, driveList[j].northing);
                    

                    if (distance < spacing  *.5)
                    {
                        
                        driveList.RemoveAt(j);
                        cnt = driveList.Count;
                        i--;

                    }
                }

                //if (maxdist < spacing) break;
            }



            for (int i = 1; i < cnt-1; i++)
            {

                driveList[i].heading = Math.Atan2(driveList[i + 1].easting - driveList[i].easting, driveList[i + 1].northing - driveList[i].northing);
                if (driveList[i].heading < 0) driveList[i].heading += glm.twoPI;
               
                
            }



        }


            private void PurePursuitRecPath(int ptCount, List<CRecPathPt> _recList)
        {
            //find the closest 2 points to current fix
            double minDistA = 9999999999;
            double dist, dx, dz;
            

            //set the search range close to current position
            int top = currentPositonIndex + 5;
            if (top > ptCount) top = ptCount;

            for (int t = currentPositonIndex; t < top; t++)
            {
                dist = ((pivotAxlePosRP.easting - _recList[t].easting) * (pivotAxlePosRP.easting - _recList[t].easting))
                                + ((pivotAxlePosRP.northing - _recList[t].northing) * (pivotAxlePosRP.northing - _recList[t].northing));
                if (dist < minDistA)
                {
                    minDistA = dist;
                    A = t;
                }
            }

            //Save the closest point
            C = A;

            double goalPointDistance = mf.vehicle.UpdateGoalPointDistance();
            if (A+1>= ptCount)
            {
                isEndOfTheRecLine = true;
                return;
            }
            double tempLook = glm.Distance(_recList[A].easting, _recList[A].northing, _recList[A+1].easting, _recList[A+1].northing);
            tempLook = goalPointDistance / tempLook;
            tempLook = Math.Round(tempLook);
            //Console.WriteLine(tempLook);
            if (tempLook < 1) tempLook = 1;

            //next point is the next in list
            B = A + (int)tempLook;
            B = A + 1;
            if (B >= ptCount)
            {
                //don't go past the end of the list - "end of the line" trigger
                B = ptCount-2;
                if (A >= ptCount-2)
                {
                    A = ptCount - 3;

                    isEndOfTheRecLine = true;
                }
            }

            //save current position
            currentPositonIndex = A;
            lookPositonIndex = B;

            //get the distance from currently active AB line
            dx = _recList[B].easting - _recList[A].easting;
            dz = _recList[B].northing - _recList[A].northing;

            if (Math.Abs(dx) < Double.Epsilon && Math.Abs(dz) < Double.Epsilon) return;

            //how far from current AB Line is fix
            distanceFromCurrentLinePivot = ((dz * pivotAxlePosRP.easting) - (dx * pivotAxlePosRP.northing) + (_recList[B].easting
                        * _recList[A].northing) - (_recList[B].northing * _recList[A].easting))
                            / Math.Sqrt((dz * dz) + (dx * dx));

            //integral slider is set to 0
            if (mf.vehicle.purePursuitIntegralGain != 0)
            {
                pivotDistanceError = distanceFromCurrentLinePivot * 0.2 + pivotDistanceError * 0.8;

                if (counter2++ > 4)
                {
                    pivotDerivative = pivotDistanceError - pivotDistanceErrorLast;
                    pivotDistanceErrorLast = pivotDistanceError;
                    counter2 = 0;
                    pivotDerivative *= 2;

                    //limit the derivative
                    //if (pivotDerivative > 0.03) pivotDerivative = 0.03;
                    //if (pivotDerivative < -0.03) pivotDerivative = -0.03;
                    //if (Math.Abs(pivotDerivative) < 0.01) pivotDerivative = 0;
                }

                //pivotErrorTotal = pivotDistanceError + pivotDerivative;

                if (isFollowingRecPath
                    && Math.Abs(pivotDerivative) < (0.1)
                    && mf.avgSpeed > 2.5)
                //&& Math.Abs(pivotDistanceError) < 0.2)

                {
                    //if over the line heading wrong way, rapidly decrease integral
                    if ((inty < 0 && distanceFromCurrentLinePivot < 0) || (inty > 0 && distanceFromCurrentLinePivot > 0))
                    {
                        inty += pivotDistanceError * mf.vehicle.purePursuitIntegralGain * -0.04;
                    }
                    else
                    {
                        if (Math.Abs(distanceFromCurrentLinePivot) > 0.02)
                        {
                            inty += pivotDistanceError * mf.vehicle.purePursuitIntegralGain * -0.02;
                            if (inty > 0.2) inty = 0.2;
                            else if (inty < -0.2) inty = -0.2;
                        }
                    }
                }
                else inty *= 0.95;
            }
            else inty = 0;

            if (mf.isReverse) inty = 0;

            // ** Pure pursuit ** - calc point on ABLine closest to current position
            double U = (((pivotAxlePosRP.easting - _recList[A].easting) * dx)
                        + ((pivotAxlePosRP.northing - _recList[A].northing) * dz))
                        / ((dx * dx) + (dz * dz));

            rEastRP = _recList[A].easting + (U * dx);
            rNorthRP = _recList[A].northing + (U * dz);

            //update base on autosteer settings and distance from line
            

            bool ReverseHeading = !mf.isReverse;

            int count = ReverseHeading ? 1 : -1;
            CRecPathPt start = new CRecPathPt(rEastRP, rNorthRP, 0, 0, false);
            double distSoFar = 0;

            for (int i = ReverseHeading ? B : A; i < ptCount && i >= 0; i += count)
            {
                // used for calculating the length squared of next segment.
                double tempDist = Math.Sqrt((start.easting - _recList[i].easting) * (start.easting - _recList[i].easting)
                    + (start.northing - _recList[i].northing) * (start.northing - _recList[i].northing));

                //will we go too far?
                if ((tempDist + distSoFar) > goalPointDistance)
                {
                    double j = (goalPointDistance - distSoFar) / tempDist; // the remainder to yet travel

                    goalPointRP.easting = (((1 - j) * start.easting) + (j * _recList[i].easting));
                    goalPointRP.northing = (((1 - j) * start.northing) + (j * _recList[i].northing));
                    break;
                }
                else distSoFar += tempDist;
                start = _recList[i];
                B = i;
            }

            //goalPointRP.easting = _recList[B].easting;
            //goalPointRP.northing = _recList[B].northing;

            //calc "D" the distance from pivotAxlePosRP axle to lookahead point
            double goalPointDistanceSquared = glm.DistanceSquared(goalPointRP.northing, goalPointRP.easting, pivotAxlePosRP.northing, pivotAxlePosRP.easting);

            //calculate the the delta x in local coordinates and steering angle degrees based on wheelbase
            double localHeading = glm.twoPI - mf.fixHeading + inty;

            ppRadiusRP = goalPointDistanceSquared / (2 * (((goalPointRP.easting - pivotAxlePosRP.easting) * Math.Cos(localHeading)) + ((goalPointRP.northing - pivotAxlePosRP.northing) * Math.Sin(localHeading))));
            
            steerAngleRP = glm.toDegrees(Math.Atan(2 * (((goalPointRP.easting - pivotAxlePosRP.easting) * Math.Cos(localHeading))
                + ((goalPointRP.northing - pivotAxlePosRP.northing) * Math.Sin(localHeading))) * mf.vehicle.wheelbase / goalPointDistanceSquared));
           
            if (ppRadiusRP < 0) steerAngleRP = glm.toDegrees(Math.Atan((mf.vehicle.wheelbase) / (ppRadiusRP)));
            else steerAngleRP = glm.toDegrees(Math.Atan((mf.vehicle.wheelbase) / (ppRadiusRP)));
           
            if (steerAngleRP < -mf.vehicle.maxSteerAngle) steerAngleRP = -mf.vehicle.maxSteerAngle;
            if (steerAngleRP > mf.vehicle.maxSteerAngle) steerAngleRP = mf.vehicle.maxSteerAngle;

            //Convert to centimeters
            mf.guidanceLineDistanceOff = (short)Math.Round(distanceFromCurrentLinePivot * 1000.0, MidpointRounding.AwayFromZero);
            mf.guidanceLineSteerAngle = (short)(steerAngleRP * 100);
            
        }

        private void PurePursuitDubins(int ptCount)
        {
            double dist, dx, dz;
            double minDistA = 1000000, minDistB = 1000000;

            int next = lastA + 10;
            if (next > ptCount) next = ptCount;
            //find the closest 2 points to current fix
            for (int t = lastA+1; t < next; t++)
            {
                dist = ((pivotAxlePosRP.easting - shuttleDubinsList[t].easting) * (pivotAxlePosRP.easting - shuttleDubinsList[t].easting))
                                + ((pivotAxlePosRP.northing - shuttleDubinsList[t].northing) * (pivotAxlePosRP.northing - shuttleDubinsList[t].northing));
                if (dist < minDistA)
                {
                    minDistB = minDistA;
                    B = A;
                    minDistA = dist;
                    A = t;
                }
                else if (dist < minDistB)
                {
                    minDistB = dist;
                    B = t;
                }
            }

            //just need to make sure the points continue ascending or heading switches all over the place
            if (A > B) { C = A; A = B; B = C; }
            double goalPointDistance = mf.vehicle.UpdateGoalPointDistance();
           
            double tempLook = glm.Distance(shuttleDubinsList[4].easting, shuttleDubinsList[4].northing, shuttleDubinsList[3].easting, shuttleDubinsList[3].northing);
            
            tempLook = goalPointDistance / tempLook;
            tempLook = Math.Round(tempLook);
            
            if (tempLook <10) tempLook =10;
            //currentLocationIndex = A;
            lastA = A;
            B = A + (int)tempLook;
            if (B >= ptCount)
            {
                //don't go past the end of the list - "end of the line" trigger
                lastA = 0;
                return;
                
            }
           

            //get the distance from currently active AB line
            dx = shuttleDubinsList[B].easting - shuttleDubinsList[A].easting;
            dz = shuttleDubinsList[B].northing - shuttleDubinsList[A].northing;

            if (Math.Abs(dx) < Double.Epsilon && Math.Abs(dz) < Double.Epsilon) return;

            //abHeading = Math.Atan2(dz, dx);

            //how far from current AB Line is fix
            distanceFromCurrentLinePivot = ((dz * pivotAxlePosRP.easting) - (dx * pivotAxlePosRP.northing) + (shuttleDubinsList[B].easting
                        * shuttleDubinsList[A].northing) - (shuttleDubinsList[B].northing * shuttleDubinsList[A].easting))
                            / Math.Sqrt((dz * dz) + (dx * dx));

            //integral slider is set to 0
            if (mf.vehicle.purePursuitIntegralGain != 0)
            {
                pivotDistanceError = distanceFromCurrentLinePivot * 0.2 + pivotDistanceError * 0.8;

                if (counter2++ > 4)
                {
                    pivotDerivative = pivotDistanceError - pivotDistanceErrorLast;
                    pivotDistanceErrorLast = pivotDistanceError;
                    counter2 = 0;
                    pivotDerivative *= 2;

                    //limit the derivative
                    //if (pivotDerivative > 0.03) pivotDerivative = 0.03;
                    //if (pivotDerivative < -0.03) pivotDerivative = -0.03;
                    //if (Math.Abs(pivotDerivative) < 0.01) pivotDerivative = 0;
                }

                //pivotErrorTotal = pivotDistanceError + pivotDerivative;

                if (mf.isAutoSteerBtnOn
                    && Math.Abs(pivotDerivative) < (0.1)
                    && mf.avgSpeed > 2.5
                    && !mf.yt.isYouTurnTriggered)

                {
                    //if over the line heading wrong way, rapidly decrease integral
                    if ((inty < 0 && distanceFromCurrentLinePivot < 0) || (inty > 0 && distanceFromCurrentLinePivot > 0))
                    {
                        inty += pivotDistanceError * mf.vehicle.purePursuitIntegralGain * -0.04;
                    }
                    else
                    {
                        if (Math.Abs(distanceFromCurrentLinePivot) > 0.02)
                        {
                            inty += pivotDistanceError * mf.vehicle.purePursuitIntegralGain * -0.02;
                            if (inty > 0.2) inty = 0.2;
                            else if (inty < -0.2) inty = -0.2;
                        }
                    }
                }
                else inty *= 0.95;
            }
            else inty = 0;

            if (mf.isReverse) inty = 0;

            // ** Pure pursuit ** - calc point on ABLine closest to current position
            double U = (((pivotAxlePosRP.easting - shuttleDubinsList[A].easting) * dx)
                        + ((pivotAxlePosRP.northing - shuttleDubinsList[A].northing) * dz))
                        / ((dx * dx) + (dz * dz));

            rEastRP = shuttleDubinsList[A].easting + (U * dx);
            rNorthRP = shuttleDubinsList[A].northing + (U * dz);

            //update base on autosteer settings and distance from line
            

            bool ReverseHeading = !mf.isReverse;

            int count = ReverseHeading ? 1 : -1;
            CRecPathPt start = new CRecPathPt(rEastRP, rNorthRP, 0, 0, false);
            double distSoFar = 0;

            for (int i = ReverseHeading ? B : A; i < ptCount && i >= 0; i += count)
            {
                // used for calculating the length squared of next segment.
                double tempDist = Math.Sqrt((start.easting - shuttleDubinsList[i].easting) * (start.easting - shuttleDubinsList[i].easting)
                    + (start.northing - shuttleDubinsList[i].northing) * (start.northing - shuttleDubinsList[i].northing));

                //will we go too far?
                if ((tempDist + distSoFar) > goalPointDistance)
                {
                    double j = (goalPointDistance - distSoFar) / tempDist; // the remainder to yet travel

                    goalPointRP.easting = (((1 - j) * start.easting) + (j * shuttleDubinsList[i].easting));
                    goalPointRP.northing = (((1 - j) * start.northing) + (j * shuttleDubinsList[i].northing));
                    break;
                }
                else distSoFar += tempDist;
                start = shuttleDubinsList[i];
            }
            //goalPointRP.easting = shuttleDubinsList[B].easting;
            //goalPointRP.northing = shuttleDubinsList[B].northing;
           
            //calc "D" the distance from pivotAxlePosRP axle to lookahead point
            double goalPointDistanceSquared = glm.DistanceSquared(goalPointRP.northing, goalPointRP.easting, pivotAxlePosRP.northing, pivotAxlePosRP.easting);

            //calculate the the delta x in local coordinates and steering angle degrees based on wheelbase
            //double localHeading = glm.twoPI - mf.fixHeading;

            double localHeading = glm.twoPI - mf.fixHeading + inty;

            ppRadiusRP = goalPointDistanceSquared / (2 * (((goalPointRP.easting - pivotAxlePosRP.easting) * Math.Cos(localHeading)) + ((goalPointRP.northing - pivotAxlePosRP.northing) * Math.Sin(localHeading))));

            steerAngleRP = glm.toDegrees(Math.Atan(2 * (((goalPointRP.easting - pivotAxlePosRP.easting) * Math.Cos(localHeading))
                + ((goalPointRP.northing - pivotAxlePosRP.northing) * Math.Sin(localHeading))) * mf.vehicle.wheelbase / goalPointDistanceSquared));

            if (ppRadiusRP < 0) steerAngleRP = glm.toDegrees(Math.Atan((mf.vehicle.wheelbase) / (ppRadiusRP + mf.vehicle.trackWidth / 2)));
            else steerAngleRP = glm.toDegrees(Math.Atan((mf.vehicle.wheelbase) / (ppRadiusRP - mf.vehicle.trackWidth / 2)));


            if (steerAngleRP < -mf.vehicle.maxSteerAngle) steerAngleRP = -mf.vehicle.maxSteerAngle;
            if (steerAngleRP > mf.vehicle.maxSteerAngle) steerAngleRP = mf.vehicle.maxSteerAngle;

            if (ppRadiusRP < -500) ppRadiusRP = -500;
            if (ppRadiusRP > 500) ppRadiusRP = 500;

            radiusPointRP.easting = pivotAxlePosRP.easting + (ppRadiusRP * Math.Cos(localHeading));
            radiusPointRP.northing = pivotAxlePosRP.northing + (ppRadiusRP * Math.Sin(localHeading));

            //angular velocity in rads/sec  = 2PI * m/sec * radians/meters
           // double angVel = glm.twoPI * 0.277777 * mf.pn.speed * (Math.Tan(glm.toRadians(steerAngleRP))) / mf.vehicle.wheelbase;

            //clamp the steering angle to not exceed safe angular velocity
            //if (Math.Abs(angVel) > mf.vehicle.maxAngularVelocity)
            //{
            //    steerAngleRP = glm.toDegrees(steerAngleRP > 0 ?
            //            (Math.Atan((mf.vehicle.wheelbase * mf.vehicle.maxAngularVelocity) / (glm.twoPI * mf.avgSpeed * 0.277777)))
            //        : (Math.Atan((mf.vehicle.wheelbase * -mf.vehicle.maxAngularVelocity) / (glm.twoPI * mf.avgSpeed * 0.277777))));
            //}

            //Convert to centimeters
            mf.guidanceLineDistanceOff = (short)Math.Round(distanceFromCurrentLinePivot * 1000.0, MidpointRounding.AwayFromZero);
            mf.guidanceLineSteerAngle = (short)(steerAngleRP * 100);
        }


        public void DrawRecordedLine()
        {

            int  ptCount = recList.Count;
            if (ptCount < 1) return;
            GL.LineWidth(1);
            GL.Color3(0.98f, 0.92f, 0.460f);
            GL.Begin(PrimitiveType.LineStrip);
            for (int h = 0; h < ptCount; h++) GL.Vertex3(recList[h].easting, recList[h].northing, 0);
            GL.End();

            ptCount = driveList.Count;
            if (ptCount > 1)
            {
                GL.LineWidth(3);
                GL.PointSize(3.0f);
                GL.Color3(0.98f, 0.0f, 0.0f);
                GL.Begin(PrimitiveType.Points);
                for (int h = 0; h < ptCount; h++)
                {
                    if (isDrivingRecordedPath)
                    {
                        if (h == B) GL.Color3(0.0f, 0.98f, 0.0f);
                        else GL.Color3(0.98f, 0.0f, 0.0f);
                    }
                    
                    if (h > currentPositonIndex)
                        GL.Vertex3(driveList[h].easting, driveList[h].northing, 0);
                }
                GL.End();
               
            }
           
            




            //if (!isRecordOn)
            //{
            //    //Draw lookahead Point
            //    GL.PointSize(16.0f);
            //    GL.Begin(PrimitiveType.Points);

            //    //GL.Color(1.0f, 1.0f, 0.25f);
            //    //GL.Vertex(rEast, rNorth, 0.0);

            //    GL.Color3(1.0f, 0.5f, 0.95f);
            //    GL.Vertex3(driveList[currentPositonIndex].easting, driveList[currentPositonIndex].northing, 0);
            //    //GL.Color3(1.0f, 0.5f, 0.0f);
            //    //GL.Vertex3(driveList[lookPositonIndex].easting, driveList[lookPositonIndex].northing, 0);

            //    GL.End();
            //    GL.PointSize(1.0f);
            //}
        }

        public void DrawDubins()
        {
            if (shuttleDubinsList.Count > 1)
            {
                //GL.LineWidth(2);
                GL.PointSize(2);
                GL.Color3(0.298f, 0.96f, 0.2960f);
                GL.Begin(PrimitiveType.Points);
                for (int h = 0; h < shuttleDubinsList.Count; h++)
                    GL.Vertex3(shuttleDubinsList[h].easting, shuttleDubinsList[h].northing, 0);
                GL.End();
               
                if(isFollowingDubinsToPath && A < shuttleDubinsList.Count&& B < shuttleDubinsList.Count)
                {
                    GL.PointSize(16.0f);
                    GL.Begin(PrimitiveType.Lines);

                    //GL.Color(1.0f, 1.0f, 0.25f);
                    //GL.Vertex(rEast, rNorth, 0.0);

                    GL.Color3(1.0f, 0.5f, 0.95f);
                    GL.Vertex3(shuttleDubinsList[A].easting, shuttleDubinsList[A].northing, 0);
                    //GL.Color3(1.0f, 0.5f, 0.0f);
                    GL.Vertex3(shuttleDubinsList[B].easting, shuttleDubinsList[B].northing, 0);

                    GL.End();
                    GL.PointSize(1.0f);
                }
            }
        }
    }
}
//private void StanleyDubinsPath(int ptCount)
//{
//    //distanceFromCurrentLine = 9999;
//    //find the closest 2 points to current fix
//    double minDistA = 9999999999;
//    for (int t = 0; t < ptCount; t++)
//    {
//        double dist = ((pivotAxlePosRP.easting - shuttleDubinsList[t].easting) * (pivotAxlePosRP.easting - shuttleDubinsList[t].easting))
//                        + ((pivotAxlePosRP.northing - shuttleDubinsList[t].northing) * (pivotAxlePosRP.northing - shuttleDubinsList[t].northing));
//        if (dist < minDistA)
//        {
//            minDistA = dist;
//            A = t;
//        }
//    }

//    //save the closest point
//    C = A;
//    //next point is the next in list
//    B = A + 1;
//    if (B == ptCount) { A--; B--; }                //don't go past the end of the list - "end of the line" trigger

//    //get the distance from currently active AB line
//    //x2-x1
//    double dx = shuttleDubinsList[B].easting - shuttleDubinsList[A].easting;
//    //z2-z1
//    double dz = shuttleDubinsList[B].northing - shuttleDubinsList[A].northing;

//    if (Math.Abs(dx) < Double.Epsilon && Math.Abs(dz) < Double.Epsilon) return;

//    //abHeading = Math.Atan2(dz, dx);
//    abHeading = shuttleDubinsList[A].heading;

//    //how far from current AB Line is fix
//    distanceFromCurrentLinePivot = ((dz * pivotAxlePosRP.easting) - (dx * pivotAxlePosRP
//        .northing) + (shuttleDubinsList[B].easting
//                * shuttleDubinsList[A].northing) - (shuttleDubinsList[B].northing * shuttleDubinsList[A].easting))
//                    / Math.Sqrt((dz * dz) + (dx * dx));

//    //are we on the right side or not
//    isOnRightSideCurrentLine = distanceFromCurrentLinePivot > 0;

//    // calc point on ABLine closest to current position
//    double U = (((pivotAxlePosRP.easting - shuttleDubinsList[A].easting) * dx)
//                + ((pivotAxlePosRP.northing - shuttleDubinsList[A].northing) * dz))
//                / ((dx * dx) + (dz * dz));

//    rEastRP = shuttleDubinsList[A].easting + (U * dx);
//    rNorthRP = shuttleDubinsList[A].northing + (U * dz);

//    //the first part of stanley is to extract heading error
//    double abFixHeadingDelta = (pivotAxlePosRP.heading - abHeading);

//    //Fix the circular error - get it from -Pi/2 to Pi/2
//    if (abFixHeadingDelta > Math.PI) abFixHeadingDelta -= Math.PI;
//    else if (abFixHeadingDelta < Math.PI) abFixHeadingDelta += Math.PI;
//    if (abFixHeadingDelta > glm.PIBy2) abFixHeadingDelta -= Math.PI;
//    else if (abFixHeadingDelta < -glm.PIBy2) abFixHeadingDelta += Math.PI;

//    //normally set to 1, less then unity gives less heading error.
//    abFixHeadingDelta *= mf.vehicle.stanleyHeadingErrorGain;
//    if (abFixHeadingDelta > 0.74) abFixHeadingDelta = 0.74;
//    if (abFixHeadingDelta < -0.74) abFixHeadingDelta = -0.74;

//    //the non linear distance error part of stanley
//    steerAngleRP = Math.Atan((distanceFromCurrentLinePivot * mf.vehicle.stanleyDistanceErrorGain) / ((mf.pn.speed * 0.277777) + 1));

//    //clamp it to max 42 degrees
//    if (steerAngleRP > 0.74) steerAngleRP = 0.74;
//    if (steerAngleRP < -0.74) steerAngleRP = -0.74;

//    //add them up and clamp to max in vehicle settings
//    steerAngleRP = glm.toDegrees((steerAngleRP + abFixHeadingDelta) * -1.0);
//    if (steerAngleRP < -mf.vehicle.maxSteerAngle) steerAngleRP = -mf.vehicle.maxSteerAngle;
//    if (steerAngleRP > mf.vehicle.maxSteerAngle) steerAngleRP = mf.vehicle.maxSteerAngle;

//    //Convert to millimeters and round properly to above/below .5
//    distanceFromCurrentLinePivot = Math.Round(distanceFromCurrentLinePivot * 1000.0, MidpointRounding.AwayFromZero);

//    //every guidance method dumps into these that are used and sent everywhere, last one wins
//    mf.guidanceLineDistanceOff = mf.distanceDisplaySteer = (Int16)distanceFromCurrentLinePivot;
//    mf.guidanceLineSteerAngle = (Int16)(steerAngleRP * 100);
//}
//private void StanleyRecPath(int ptCount)
//{
//    //find the closest 2 points to current fix
//    double minDistA = 9999999999;

//    //set the search range close to current position
//    int top = currentPositonIndex + 5;
//    if (top > ptCount) top = ptCount;

//    double dist;
//    for (int t = currentPositonIndex; t < top; t++)
//    {
//        dist = ((pivotAxlePosRP.easting - recList[t].easting) * (pivotAxlePosRP.easting - recList[t].easting))
//                        + ((pivotAxlePosRP.northing - recList[t].northing) * (pivotAxlePosRP.northing - recList[t].northing));
//        if (dist < minDistA)
//        {
//            minDistA = dist;
//            A = t;
//        }
//    }

//    //Save the closest point
//    C = A;

//    //next point is the next in list
//    B = A + 1;
//    if (B == ptCount)
//    {
//        //don't go past the end of the list - "end of the line" trigger
//        A--;
//        B--;
//        isEndOfTheRecLine = true;
//    }

//    //save current position
//    currentPositonIndex = A;

//    //get the distance from currently active AB line
//    double dx = recList[B].easting - recList[A].easting;
//    double dz = recList[B].northing - recList[A].northing;

//    if (Math.Abs(dx) < Double.Epsilon && Math.Abs(dz) < Double.Epsilon) return;

//    abHeading = Math.Atan2(dx, dz);
//    //abHeading = recList[A].heading;

//    //how far from current AB Line is fix
//    distanceFromCurrentLinePivot =
//        ((dz * pivotAxlePosRP.easting) - (dx * pivotAxlePosRP.northing) + (recList[B].easting
//                * recList[A].northing) - (recList[B].northing * recList[A].easting))
//                    / Math.Sqrt((dz * dz) + (dx * dx));

//    //are we on the right side or not
//    isOnRightSideCurrentLine = distanceFromCurrentLinePivot > 0;

//    // calc point on ABLine closest to current position
//    double U = (((pivotAxlePosRP.easting - recList[A].easting) * dx)
//                + ((pivotAxlePosRP.northing - recList[A].northing) * dz))
//                / ((dx * dx) + (dz * dz));

//    rEastRP = recList[A].easting + (U * dx);
//    rNorthRP = recList[A].northing + (U * dz);

//    //the first part of stanley is to extract heading error
//    double abFixHeadingDelta = (pivotAxlePosRP.heading - abHeading);

//    //Fix the circular error - get it from -Pi/2 to Pi/2
//    if (abFixHeadingDelta > Math.PI) abFixHeadingDelta -= Math.PI;
//    else if (abFixHeadingDelta < Math.PI) abFixHeadingDelta += Math.PI;
//    if (abFixHeadingDelta > glm.PIBy2) abFixHeadingDelta -= Math.PI;
//    else if (abFixHeadingDelta < -glm.PIBy2) abFixHeadingDelta += Math.PI;

//    //normally set to 1, less then unity gives less heading error.
//    abFixHeadingDelta *= mf.vehicle.stanleyHeadingErrorGain;
//    if (abFixHeadingDelta > 0.74) abFixHeadingDelta = 0.74;
//    if (abFixHeadingDelta < -0.74) abFixHeadingDelta = -0.74;

//    //the non linear distance error part of stanley
//    steerAngleRP = Math.Atan((distanceFromCurrentLinePivot * mf.vehicle.stanleyDistanceErrorGain) / ((mf.pn.speed * 0.277777) + 1));

//    //clamp it to max 42 degrees
//    if (steerAngleRP > 0.74) steerAngleRP = 0.74;
//    if (steerAngleRP < -0.74) steerAngleRP = -0.74;

//    //add them up and clamp to max in vehicle settings
//    steerAngleRP = glm.toDegrees((steerAngleRP + abFixHeadingDelta) * -1.0);
//    if (steerAngleRP < -mf.vehicle.maxSteerAngle) steerAngleRP = -mf.vehicle.maxSteerAngle;
//    if (steerAngleRP > mf.vehicle.maxSteerAngle) steerAngleRP = mf.vehicle.maxSteerAngle;

//    //Convert to millimeters and round properly to above/below .5
//    distanceFromCurrentLinePivot = Math.Round(distanceFromCurrentLinePivot * 1000.0, MidpointRounding.AwayFromZero);

//    //every guidance method dumps into these that are used and sent everywhere, last one wins
//    mf.guidanceLineDistanceOff = mf.distanceDisplaySteer = (Int16)distanceFromCurrentLinePivot;
//    mf.guidanceLineSteerAngle = (Int16)(steerAngleRP * 100);
//}