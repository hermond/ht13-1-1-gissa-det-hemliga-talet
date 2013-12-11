using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SecretNumber.Models
{

    public class SecretNumber
    {
        private List<GuessedNumber> _guessedNumbers;
        private GuessedNumber _lastGuessedNumber;
        private int? _number;
        private const int maxNumberOfGuesses = 7;

        public bool CanMakeGuess {
            get
            {
                if (Count < maxNumberOfGuesses)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }

            }
        public int Count { get { return _guessedNumbers.Count; } }
        public IList<GuessedNumber> GuessedNumbers
        {
            get
            {
                return _guessedNumbers.AsReadOnly();
            }
        
        
        }
        public GuessedNumber LastGuessedNumber {
            get
            {
                return _lastGuessedNumber;
            }
        }
        public int? Number
        {
            get
            {
                if (Count == maxNumberOfGuesses)
                {
                    return _number;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                _number = value;
            }
        }

        public SecretNumber()
        {
          
            Random random = new Random();
            Number = random.Next(1, 100);
            Initialize();
        }


        public void Initialize()
        {

            _guessedNumbers = new List<GuessedNumber>();
            _lastGuessedNumber = new GuessedNumber();


        }

        public Outcome MakeGuess(int guess)
        {
            
            if (guess < 0 || guess > 100)
            {
                throw new ArgumentOutOfRangeException();    
            }
            if(!CanMakeGuess)
            {
                return Outcome.NoMoreGuesses;
            }
            else if(_guessedNumbers.Exists(item => item.Number == guess))
            {
                return Outcome.OldGuess;
            }
            else
            {
                _lastGuessedNumber.Number = guess;
                Count++;
                if (guess == _number)
                {
                    _lastGuessedNumber.Outcome = Outcome.Right;
                    _guessedNumbers.Add(_lastGuessedNumber);
                    return Outcome.Right;
                }
                else if (guess < _number)
                {
                    _lastGuessedNumber.Outcome = Outcome.Low;
                    _guessedNumbers.Add(_lastGuessedNumber);
                    return Outcome.Low;
                }
                else if (guess > _number)
                {
                    _lastGuessedNumber.Outcome = Outcome.High;
                    _guessedNumbers.Add(_lastGuessedNumber);
                    return Outcome.High;
                }
                else
                {
                    return Outcome.Indefinite;
                }
            }
        }
    }
}