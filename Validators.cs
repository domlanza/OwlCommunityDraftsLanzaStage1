using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OwlCommunityMemberLanzaDrafts
{
    [Serializable()]
    public class Validators
    {

        public Validators()
        {
        }


        public static bool ValidateOwlMember(frmOwlCommunity f)
        {
            if (ValidateOwlMemberID(f.txtOwlMemberID.Text) && ValidateOwlMemberName(f.txtOwlMemberName.Text))
                return true;
            else
                return false;
        }

        //Validate OwlMember ID
        public static bool ValidateOwlMemberID(string ID)
        {
            if (ID == "")
            {
                MessageBox.Show("Owl Member ID has been left blank." + "\n" +
                                "Please enter the Owl Member ID.",
                                "Empty Owl Member ID");
                return false;
            }
            if (ID.Length != 9)
            {
                MessageBox.Show("The Owl Member ID that was entered does not exactly match the 9 digit requirement." + "\n" +
                                "Please re-enter the Owl Member ID",
                                "Invalid ID Length");
                return false;
            }

            return true;
        }   // End ValidateOwlMemberID

        public static bool ValidateOwlMemberName(string name)
        {
            if (name == "")
            {
                MessageBox.Show("Owl Member Name has been left blank." + "\n" +
                                "Please enter the Owl Member Name.",
                                "Empty Owl Member Name");
                return false;
            }
            return true;
        }   // End ValidateOwlMemberName

        public static bool ValidateStudent(frmOwlCommunity f)
        {

            if (f.txtStudentGPA.Text == "" || f.txtStudentGPA.Text == null)
            {
                MessageBox.Show("Student GPA has been left blank." + "\n" +
                               "Please enter the Student GPA.",
                               "Empty Student GPA");
                return false;
            }
            else if (f.txtStudentMajor.Text == "" || f.txtStudentMajor.Text == null)
            {
                MessageBox.Show("Student Major has been left blank." + "\n" +
                               "Please enter the Student Major.",
                               "Empty Student Major");
                return false;
            }
            else
            {
                try
                {
                    decimal gpa = Convert.ToDecimal(f.txtStudentGPA.Text);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Student GPA's input can not be accepted");
                    return false;
                }
            }
            return true;
        }

        public static bool ValidateUndergraduateStudent(frmOwlCommunity f)
        {
            if (ValidateUndergraduateStudentTution(f.txtUndergraduateStudentTuition.Text) && ValidateUndergraduateStudentCredits(f.txtUndergraduateStudentCredits.Text))
                return true;
            else
                return false;
        }

        public static bool ValidateUndergraduateStudentTution(string tuition)
        {
            if (tuition == "" || tuition == null)
            {
                MessageBox.Show("No value present for Tution");
                return false;
            }
            else
            {
                try
                {
                    decimal t = Convert.ToDecimal(tuition);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Student tuition can not be accepted");
                    return false;
                }
            }
            return true;
        }

        public static bool ValidateUndergraduateStudentCredits(string credits)
        {
            if (credits == "" || credits == null)
            {
                MessageBox.Show("No value present for Credits");
                return false;
            }
            else
            {
                try
                {
                    int c = Convert.ToInt32(credits);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Student credits can not be accepted");
                    return false;
                }
            }
            return true;
        }

        //Validate Graduate Stipend
        private decimal gradStipend;
        public bool ValidateGraduateStipend(string stipend) {
            if (String.IsNullOrEmpty(stipend))
            {
                MessageBox.Show("A stipend amount is required.");
                return false;
            }
            else
            {
                try
                {
                    decimal tempStipend = Convert.ToDecimal(stipend);
                    if (tempStipend < 0)
                    {
                        MessageBox.Show("This stipend must be greater than 0.");
                        return false;
                    }
                    else
                    {
                        gradStipend = tempStipend;
                        return true;
                    }
                }
                catch {
                    MessageBox.Show("The stipend must be an valid decimal");
                    return false;
                }
            }
        }


        //Validate Chair Stipend
        private decimal chairStipend;
        public bool ValidateChairpersonStipend(string stipend)
        {
            if (String.IsNullOrEmpty(stipend))
            {
                MessageBox.Show("A stipend amount is required.");
                return false;
            }
            else
            {
                try
                {
                    decimal tempStipend = Convert.ToDecimal(stipend);
                    if (tempStipend < 0)
                    {
                        MessageBox.Show("This stipend must be greater than 0.");
                        return false;
                    }
                    else
                    {
                        chairStipend = tempStipend;
                        return true;
                    }
                }
                catch
                {
                    MessageBox.Show("The stipend must be an valid decimal");
                    return false;
                }
            }
        }

        private string facultyDepartment;
        public bool ValidateDepartment(string strDept)
        {
            if (String.IsNullOrEmpty(strDept))
            {
                MessageBox.Show("Department is required");
                return false;
            }
            else
            {
                facultyDepartment = strDept;
                return true;
            }
        }

        //tuition, id, credits, gpa, 
        private int id;
        public bool ValidateID(string strID)
        {
            if (String.IsNullOrEmpty(strID))
            {
                MessageBox.Show("Please enter an interger ID of length 9");
                return false;
            }
            else
            {
                try
                {
                    id = Convert.ToInt32(strID);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        //Valid tuition
        private decimal studentTuition;

        public bool ValidateTuition(string tuition)
        {
            if (String.IsNullOrEmpty(tuition))
            {
                MessageBox.Show("Tuition is required");
                return false;
            }
            else
            {
                try
                {
                    decimal tempTuition = Convert.ToDecimal(tuition);
                    studentTuition = tempTuition;
                    return true;
                }
                catch {
                    MessageBox.Show("Student tuition must be a decimal");
                    return false;
                }
            }
        }

        // Validate Credits 
        private int studentCredits;
        public bool ValidateCredits(string credits)
        {
            if (String.IsNullOrEmpty(credits))
            {
                MessageBox.Show("The amount of credits is required.");

                return false;
            }
            else
            {
                try
                {
                    int tempCredits = Convert.ToInt16(credits);
                    if (tempCredits < 0)
                    {
                        MessageBox.Show("The amount of credits cannot be less than 0.");
                        return false;
                    }
                    else
                    {
                        studentCredits = tempCredits;
                        return true;
                    }
                }
                catch
                {
                    MessageBox.Show("Student credits must be an integer.");
                    return false;
                }
            }
        }
        // Validate GPA
        private decimal studentGPA;
        public bool ValidGPA(string GPA)
        {
            if (String.IsNullOrEmpty(GPA))
            {
                MessageBox.Show("GPA is required.");
                return false;
            }
            else
            {
                try
                {

                    studentGPA = Convert.ToDecimal(GPA);
                    return true;

                }
                catch
                {
                    MessageBox.Show("Please enter a valid GPA.");
                    return false;
                }
            }
        }
        private string studentName;
        public bool ValidateName(string Name)
        {
            if (String.IsNullOrEmpty(Name))
            {
                MessageBox.Show("Name field needs to be filled");
                return false;
            }
            else {
                try
                {
                    studentName = Name;
                    return true;
                }
                catch
                {
                    MessageBox.Show("Please enter a Valid name");
                    return false;

                }
            }
        }
        private string studentYear;
        public bool ValidateYear(string year)
        {
            if (String.IsNullOrEmpty(year))
            {
                MessageBox.Show("Year field needs to be filled");
                return false;
            }
            else {
                try
                {
                    studentYear = year;
                    return true;
                }
                catch {
                    MessageBox.Show("Year field needs to be filled");
                    return false;
                }

            }
        }

        private string studentMajor;
        public bool ValidateMajor(string major)
        {
            if (String.IsNullOrEmpty(major))
            {
                MessageBox.Show("Major field needs to be filled");
                return false;
            }
            else
            {
                try
                {
                    studentMajor = major;
                    return true;
                }
                catch
                {
                    MessageBox.Show("Major field needs to be filled");
                    return false;
                }

            }
        }

        private string degreeProgram;
        public bool ValidateDegreeProgram(string deegreeprog)
        {
            if (String.IsNullOrEmpty(deegreeprog))
            {
                MessageBox.Show("Degree Program field needs to be filled");
                return false;
            }
            else
            {
                try
                {
                    degreeProgram = deegreeprog;
                    return true;
                }
                catch
                {
                    MessageBox.Show("Degree Program field needs to be filled");
                    return false;
                }

            }
        }

        private string RankProgram;
        public bool ValidateRank(string rank)
        {
            if (String.IsNullOrEmpty(rank))
            {
                MessageBox.Show("Rank field needs to be filled");
                return false;
            }
            else
            {
                try
                {
                    RankProgram = rank;
                    return true;
                }
                catch
                {
                    MessageBox.Show("Rank field needs to be filled");
                    return false;
                }

            }
        }



    }


   
}
