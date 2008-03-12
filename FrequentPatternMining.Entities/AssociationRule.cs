using System;
using System.Collections.Generic;
using System.Text;

namespace FrequentPatternMining.Entities
{
    /// <summary>
    /// Representation an Association Rule obtained from a frequent pattern.
    /// It's composed of two part: an antecedent (leftSide) and a consequent (rightSide) represented through ItemSet;
    /// An Association Rule is like an implication:  means that if itemset A occurs in a transaction,
    /// than itemset B also occurs with high probability. 
    /// This probability is given by the Confidence of the rule.     
    /// Confidence is an important property of an association rule. The confidence of a rule A=>B is a probability measure
    /// calculated using the support of itemset {A,B} divided by the support of {A}.
    /// Confidence (A => B) = Probability (B|A) = Support (A, B)/ Support (A)    
    /// </summary>
    
    public class AssociationRule
    {
        
        private ItemSet _leftSide;
        private ItemSet _rightSide;    
        private Double _confidence;
        private Double _support;


        /// <summary>
        /// Get or Set the association rule confidence
        /// </summary>
        public Double Confidence
        {
            get { return _confidence; }
            set { _confidence = value; }
        }

        /// <summary>
        /// Get or Set the association rule support
        /// </summary>
        public Double Support
        {
            get { return _support; }
            set { _support = value; }
        }

        /// <summary>
        /// Get or Set the left side of the current association rule 
        /// </summary>
        public ItemSet LeftSide
        {
            get { return _leftSide; }
            set { _leftSide = value; }
        }

        /// <summary>
        /// Get or Set the right side of the current association rule 
        /// </summary>
        public ItemSet RightSide
        {
            get { return _rightSide; }
            set { _rightSide = value; }
        }
    }
}
