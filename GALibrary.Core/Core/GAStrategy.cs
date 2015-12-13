using System;
using System.Collections.Generic;
using System.Text;

namespace GALibrary.Core
{
    public abstract class GAStrategy<T> where T : Chromosome
    {
        #region Storage

        private IChromosomeGenerator<T> m_chromosomeGenerator;
        private IStopCondition m_stopCondition;
        private IJudgeStrategy<T> m_judgeStrategy;
        private ISelectionStrategy<T> m_selectionStrategy;
        private ICrossoverStrategy<T> m_crossoverStrategy;
        private IMutationStrategy<T> m_mutationStrategy;
        private T[] m_population;

        #endregion

        #region Events

        public event EventHandler ReportProgress;

        #endregion

        #region Construction

        public GAStrategy(int populationSize)
        {
            m_population = new T[populationSize];
        }

        #endregion

        #region Properties

        public T[] Population
        {
            get
            {
                return m_population;
            }
            protected set
            {
                m_population = value;
            }
        }

        public IChromosomeGenerator<T> ChromosomeGenerator
        {
            get
            {
                return m_chromosomeGenerator;
            }
            set
            {
                m_chromosomeGenerator = value;
            }
        }

        public IStopCondition StopCondition
        {
            get
            {
                return m_stopCondition;
            }
            set
            {
                m_stopCondition = value;
            }
        }

        public IJudgeStrategy<T> JudgeStrategy
        {
            get
            {
                return m_judgeStrategy;
            }
            set
            {
                m_judgeStrategy = value;
            }
        }

        public ISelectionStrategy<T> SelectionStrategy
        {
            get
            {
                return m_selectionStrategy;
            }
            set
            {
                m_selectionStrategy = value;
            }
        }

        public ICrossoverStrategy<T> CrossoverStrategy
        {
            get
            {
                return m_crossoverStrategy;
            }
            set
            {
                m_crossoverStrategy = value;
            }
        }

        public IMutationStrategy<T> MutationStrategy
        {
            get
            {
                return m_mutationStrategy;
            }
            set
            {
                m_mutationStrategy = value;
            }
        }

        #endregion

        #region Notification methods

        protected void NotifyReportProgress()
        {
            NotifyReportProgress(EventArgs.Empty);
        }

        protected void NotifyReportProgress(EventArgs e)
        {
            if (ReportProgress != null)
                ReportProgress(this, e);
        }

        #endregion

        #region Abstract methods

        abstract public void Run();

        #endregion
    }
}
