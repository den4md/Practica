using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Polls.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum AnswerType
    {
        [Description("Один ответ")]
        one,
        [Description("Несколько ответов")]
        many,
        [Description("Свободный ответ")]
        text
    }

    public class Slide
    {
        public int slideNumber { get; set; }
        public string question { get; set; }
        public AnswerType answerType { get; set; }
        public bool isOptional { get; set; }
        public bool isPermittedCustom { get; set; }
        public string image { get; set; }
        public List<Answer> answers { get; set; }

        public Slide(int slideNumber = 0, string question = "")
        {
            this.slideNumber = slideNumber;
            this.question = question;
            answerType = AnswerType.one;
            isOptional = false;
            isPermittedCustom = false;
            image = null;
            answers = new List<Answer> { new Answer(0), new Answer(1)};
            answers[0].value = "Вариант №1";
            answers[1].value = "Вариант №2";
        }

        public void CreateNewAnswer()
        {
            if (answers.Count.Equals(10))
            {
                throw new Exception("Answers count is already 10");
            }
            answers.Add(new Answer(answers.Count));
        }

        //public void DeleteAnswer(int answerNumber)
        //{
            
        //}

        //public Answer GetAnswer(int answerNumber)
        //{
        //    return answers[answerNumber];
        //}

        public void PrepareForRequest()
        {
            if (isPermittedCustom)
                answers.Add(new Answer(answers.Count, true));
        }

        private bool makeError(string s)
        {
            MessageBox.Show(s, "Твоя жизнь", MessageBoxButtons.OK);

            return false;
        }

        public bool Validate()
        {
            if (question == null || question.Trim().Equals(""))
                return makeError($"У слайда №{slideNumber} должен быть не пустой вопрос");
            if (slideNumber < 0 || slideNumber > 50)
                return makeError($"Номер слайда - {slideNumber} - выходит за пределы");


            return ValidateAnswers();
        }

        private bool ValidateAnswers()
        {
            bool result = true;
            foreach (Answer answer in answers)
                result = result && answer.Validate();

            return result;
        }
    }
}
