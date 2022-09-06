/*
* Name: Yuheng Zhu
* Program: Business Information Technology
* Course: ADEV-1008 Programming 2
* Created: 2022-01-18
* Updated: 2022-01-19
*/

/**
 * This program represents accessories enumeration of a vehicle.
 *
 * @author Yuheng Zhu
 * @version 1.0.0
 */
namespace Zhu.Yuheng.Business
{
    /// <summary>
    /// The accessories of a vehicle.
    /// </summary>
    public enum Accessories
    {
        // The stereo system accessory.
        SteroSystem = 0,

        // The leather interior accessory.
        LeatherInterior = 1,

        // The stereo system and leather interior accessories.
        StereoAndLeather = 2,

        // The computer navigation accessory.
        ComputerNavigation = 3,

        // The stereo system and computer navigation accessories.
        StereoAndNavigation = 4,

        // The leather interior and computer navigation accessories.
        LeatherAndNavigation = 5,

        // All the accessories.
        All = 6,

        // None of the accessories.
        None = 7
    }
}