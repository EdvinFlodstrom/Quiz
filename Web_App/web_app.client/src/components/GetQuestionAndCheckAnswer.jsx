import { hasFormSubmit } from "@testing-library/user-event/dist/utils";
import React, { useState } from "react"; 
// TODO : Disable 'get question' when quiz is done.
// TODO : Clear label for inputting free-form answer when done.
// TODO : Fix player being able to submit multiple answers for free-form questions.

const GetQuestionAndCheckAnswer = ({ name, numQuestions }) => {
    const buttonEnabledClassName = "instruction-button";
    const buttonDisabledClassName = "instruction-button-disabled";

    const [initButtonDisabled, setInitButtonDisabled] = useState(false);
    const [getQuestionButtonDisabled, setGetQuestionButtonDisabled] = useState({
        disabled: false,
        className: buttonEnabledClassName,
    });
    const [submitAnswerButtonDisabled, setSubmitAnswerButtonDisabled] = useState({
        disabled: true,
        className: buttonDisabledClassName,
        hasSubmittedAnswer: false,
    });
    const [displayAnswer, setDisplayAnswer] = useState(false);
    const [question, setQuestion] = useState(null);
    const [writtenAnswer, setWrittenAnswer] = useState('');
    const [displayCorrectOrIncorrect, setDisplayCorrectOrIncorrect] = useState({
        display: false,
        correctOrIncorrectString: '',
    });

    const handleInitializeQuiz = async () => {
        try {
            await fetch(`https://localhost:7140/api/quiz/initquiz/${name}/${numQuestions}`);
            setInitButtonDisabled(true);
          } catch (error) {
            console.error('Initialization error:', error);
          }
    };

    const handleGetQuestion = async () => {
        setGetQuestionButtonDisabled({
            disabled: true,
            className: buttonDisabledClassName,
        });
        setDisplayCorrectOrIncorrect({
            display: false,
            correctOrIncorrectString: '',
        });
        
        const response = await fetch(`https://localhost:7140/api/quiz/takequiz/${name}`);
        const retrievedQuestion = await response.json();
        setQuestion(retrievedQuestion);
        setDisplayAnswer(true);
    };

    const handleSubmitAnswer = async (answer) => {
        setSubmitAnswerButtonDisabled({
            disabled: true,
            className: buttonDisabledClassName,
            hasSubmittedAnswer: true,
        });
        const response = await fetch(`https://localhost:7140/api/quiz/takequiz/${name}/${answer}`);
        const data = await response.json();
        setDisplayCorrectOrIncorrect({
            display: true,
            correctOrIncorrectString: data,
        });
        setGetQuestionButtonDisabled({
            disabled: false,
            className: buttonEnabledClassName,
        });
    };

    const handleInputChange = (e) => {
        if (submitAnswerButtonDisabled.hasSubmittedAnswer)
        {
            return;
        }
        const inputValue = e.target.value;
        setWrittenAnswer(inputValue);
        console.log(submitAnswerButtonDisabled.hasSubmittedAnswer)
        setSubmitAnswerButtonDisabled({
            disabled: inputValue.trim().length === 0 && !submitAnswerButtonDisabled.hasSubmittedAnswer,
            className: (inputValue.trim().length === 0 && !submitAnswerButtonDisabled.hasSubmittedAnswer) ? buttonDisabledClassName : buttonEnabledClassName,
            hasSubmittedAnswer: submitAnswerButtonDisabled.hasSubmittedAnswer,
        });
    };

    return (
        <>

        {!initButtonDisabled ? (
            <button 
            className={buttonEnabledClassName}
            onClick={handleInitializeQuiz}
            disabled={initButtonDisabled}>
                Initialize Quiz
            </button>
        )
        : (
             <button
             className={getQuestionButtonDisabled.className}
             onClick={handleGetQuestion}
             disabled={getQuestionButtonDisabled.disabled}>
                Get Question
             </button>
            )}

        {displayAnswer && question && (
            <>

                <h3>{question.questionText}</h3>

                {question.questionType === 'QuestionCard' && (
                    <>
                        <label htmlFor="writtenAnswer">Input answer:</label>
                        <input
                            type="text"
                            id="writtenAnswer"
                            value={writtenAnswer}
                            onChange={handleInputChange}
                            disabled={submitAnswerButtonDisabled.hasSubmittedAnswer}
                        />
                        <button
                            className={submitAnswerButtonDisabled.className}
                            disabled={submitAnswerButtonDisabled.disabled}
                            onClick={() => handleSubmitAnswer(writtenAnswer)}
                        >
                            Submit Answer
                        </button>
                    </>
                )}

                {question.questionType === "MCSACard" && (
                    <div>
                        {[question.option1, question.option2, question.option3, question.option4, question.option5].map((option, index) => (
                            <button
                                key={index}
                                className={submitAnswerButtonDisabled.className}
                                disabled={submitAnswerButtonDisabled.disabled}
                                onClick={() => handleSubmitAnswer(index + 1)}
                            >
                                {option}
                            </button>
                        ))}
                    </div>
                )}

                {displayCorrectOrIncorrect.correctOrIncorrectString && (
                    <p>{displayCorrectOrIncorrect.correctOrIncorrectString}</p>
                )}

            </>
        )}
        
        </>
    );
};

export default GetQuestionAndCheckAnswer;