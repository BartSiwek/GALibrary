using System;
using System.Collections.Generic;
using System.Text;
using GALibrary.Core;
using GALibrary.Probability;

namespace GALibrary.Common
{
    public class TournamentSelection<T> : ISelectionStrategy<T>
        where T : Chromosome
    {
        #region Storage

        private int m_tournamentSize;

        #endregion

        #region Construction

        public TournamentSelection(int tournamentSize)
        {
            if (tournamentSize <= 1)
                throw new ArgumentOutOfRangeException("tournamentSize", tournamentSize, "The tournament size must be at least 2");
            m_tournamentSize = tournamentSize;
        }

        public TournamentSelection()
            : this(2)
        { }

        #endregion

        #region Properties

        public int TournamentSize
        {
            get { return m_tournamentSize; }
            set
            {
                if (value <= 1)
                    throw new ArgumentOutOfRangeException("value", value, "The tournament size must be at least 2");
                m_tournamentSize = value;
            }
        }

        #endregion

        #region ISelectionStrategy<T> Members

        public T[] Select(T[] population, int genePoolSize)
        {
            if (genePoolSize <= 0)
                throw new ArgumentOutOfRangeException("genePoolSize", genePoolSize, "Gene pool size must be either greater than zero");

            T[] genePool = new T[genePoolSize];
            T[] tournament = new T[m_tournamentSize];

            for (int i = 0; i < genePoolSize; i++)
            {
                for (int j = 0; j < m_tournamentSize; j++)
                    tournament[j] = population[RandomNumberGenerator.Next(0, population.Length)];

                int max = 0;
                for (int j = 1; j < m_tournamentSize; j++)
                    if (tournament[max].Fitness < tournament[j].Fitness)
                        max = j;

                genePool[i] = (T)tournament[max].Clone();
            }

            return genePool;
        }

        public void Initialize()
        { }

        #endregion
    }
}
