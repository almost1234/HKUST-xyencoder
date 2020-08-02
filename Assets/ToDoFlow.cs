using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToDoFlow : MonoBehaviour
{
   //There is nothing here but a note that remind steven short term brain
   /*
    As what Danny said, there will be 200 data being shot per second, and I need to reduce it termendously as plotting with approximately
    3k dots will kill the computer. As such, I need to reduce the incoming text.

    JSON Data flow
    ------------------------------------------------
    When the first data is received, it will initialize a new list with the PointData class, which contains the x and y.
    Afterwards, the rest of the point will be checked in comparison with the previous point. If the previous point has a difference only in one
    axis, the point is replaced with the previous one. Wheras if the point is completely different with the previous coordinate in X and Y axis,
    a new point will be generated instead. With this, it will reduce the need to plot a lot of point on the UI, which takes most of the load, and
    create a more curvier graph(?)

    when the setData is changed, it will halt the reading and restart the application for the TrackingUI. The UI will be transfered to CheckingUI,
    in which people can check the pathing and the line in between. As such, when generated, each PoolableObject will be used and given the coordinates to 
    position it on the Transform. Afterwards, (idk how heavy is this) creating a line in between all of the graphs. The function also include a dual slider
    that increases or decreases the number of points depending on the time taken.

    Since there is 2 function, We need to do it one by one now

    Firstly is the data. There should be 3 states. Firstly is the initial state, a state when it doesnt move at all. During this state, the fixedUpdate will
    still receive the data, and convert it to the PointData and check if the state is changed

    Next is the start State. 
     
     
     */
}
