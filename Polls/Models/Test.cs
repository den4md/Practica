using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Polls.Models
{
    public class Test
    {
        public string name { get; set; }
        public string description { get; set; }
        public bool isAnonym { get; set; }
        public bool isPrivate { get; set; }
        public bool isRepeatable { get; set; }
        public string completingLimit { get; set; }
        public List<string> tagNames { get; set; }
        public List<Slide> slides { get; set; }

        public Test()
        {
            name = "Опрос";
            description = "";
            isAnonym = true;
            isPrivate = true;
            isRepeatable = true;
            SetCompletingTime(23, 59);
            tagNames = new List<string>();
            slides = new List<Slide>();

            //CreateNewSlide();
        }

        public void SetCompletingTime(int hours, int minutes)
        {
            if ((hours < 0) || (hours > 23) || (hours == 0 && (minutes < 5) && (minutes >= 0))
                || (minutes < 0) || (minutes > 59))
            {
                throw new Exception("Invalid data");
            }
            else
            {
                completingLimit = string.Concat(timeFormat(hours), ':', timeFormat(minutes));
            }
        }

        private string timeFormat(int value)
        {
            if (value >= 0 && value < 10)
                return "0" + value.ToString();
            return value.ToString();
        }

        //public Slide CreateNewSlide()
        //{
        //    return null;
        //}

        //public void DeleteSlide()
        //{
            
        //}

        public Slide GetSlide(int slideNumber)
        {
            return slides[slideNumber];
        }

        public void PrepareForRequest()
        {
            // mb for numerating slides/answers
        }

        private bool makeError(string s)
        {
            MessageBox.Show(s, "Ошибка", MessageBoxButtons.OK);

            return false;
        }

        public bool Validate()  // change to bool
        {
            if (name == null || name.Trim().Equals(""))
                return makeError("У теста должно быть непустое название");

            return ValidateSlides();
        }

        private bool ValidateSlides()
        {
            // Validate each slide separately
            foreach (Slide slide in slides)
            {
                slide.Validate();
            }



            // Validate slides' logic
            if (!validateLogic())
               return makeError("Проблемы с логикой слайдов (есть недостижимые)");

            return true;
        }

        private bool validateLogic()    // thieved from the server
        {
            int[] slideUsage = new int[slides.Count];  //Сколько раз заходили в слайд
            int[] slideChild = new int[slides.Count];  //Сколько детей слайда обошли
            List<int> path = new List<int>();

            // Ведущий слайд
            Slide slide = getSlideByNumber(slides, 0);
            if (slide == null)
            {
                return false;
            }

            path.Add(slide.slideNumber);

            while (canMove(path, slideChild, slide))
            {
                slideUsage[slides.IndexOf(slide)]++;

                //            Если бесконечный цикл, то тут добавить
                //            if (slideUsage[slides.indexOf(slide)] > 1 + howMuchChild(slide))
                //            {
                //                break;
                //            }

                if (howManyChild(slide) > slideChild[slide.slideNumber])
                {
                    slideChild[slides.IndexOf(slide)]++;
                    if (path.Contains(slide.answers[slideChild[slides.IndexOf(slide)] - 1].nextSlideNumber))
                    {
                        return false;
                    }

                    // проверка на случай, если
                    // getSlideByNumber(slides, slide.getAnswers()
                    // .get(slideChild[slides.indexOf(slide)] - 1).getNextSlideNumber())
                    // выдает null

                    if (slide.answers[slideChild[slides.IndexOf(slide)] - 1].nextSlideNumber != -1 &&
                            slideUsage[slides.IndexOf(getSlideByNumber(slides,
                                    slide.answers[slideChild[slides.IndexOf(slide)] - 1].nextSlideNumber))] == 0)
                    {
                        slide = getSlideByNumber(slides,
                                slide.answers[slideChild[slides.IndexOf(slide)] - 1].nextSlideNumber);
                        path.Add(slide.slideNumber);
                    }
                    continue;
                }

                path.Remove(slide.slideNumber);
                if (path.Count > 0)
                {
                    slide = getSlideByNumber(slides, path[path.Count - 1]);
                }
            }

            foreach (Slide slide1 in slides)
            {
                if (slideUsage[slides.IndexOf(slide1)] != 1 + howManyChild(slide1))
                {
                    return false;
                }
            }

            return true;
        }

        private bool canMove(List<int> path, int[] slideChild, Slide slide)
        {
            return (howManyChild(slide) > slideChild[slide.slideNumber]) || (path.Count > 0);
        }

        private int howManyChild(Slide slide)
        {
            return slide.answers.Count;
        }

        private Slide getSlideByNumber(List<Slide> slides, int number)
        {
            Slide resultSlide = null;
            foreach (Slide slide in slides)
            {
                if (slide.slideNumber == number)
                {
                    resultSlide = slide;
                    break;
                }
            }
            return resultSlide;
        }

        public bool ShouldSerializetagNames()
        {
            return (!tagNames.Count.Equals(0));
        }
    }
}
