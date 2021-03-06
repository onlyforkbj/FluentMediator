using System.Collections.Generic;
using System.Collections.ObjectModel;
using FluentMediator.Pipelines;
using FluentMediator.Pipelines.CancellablePipelineAsync;
using FluentMediator.Pipelines.Pipeline;
using FluentMediator.Pipelines.PipelineAsync;

namespace FluentMediator
{
    /// <summary>
    /// PipelineProvider Builder
    /// </summary>
    public class PipelineProviderBuilder : IPipelineProviderBuilder
    {
        private ICollection<IPipelineBuilder> _pipelineBuilderCollection { get; }
        private ICollection<IPipelineAsyncBuilder> _asyncPipelineBuilderCollection { get; }
        private ICollection<ICancellablePipelineAsyncBuilder> _cancellablePipelineBuilderCollection { get; }

        /// <summary>
        /// Instantiate a PipelineProviderBuilder
        /// </summary>
        public PipelineProviderBuilder()
        {
            _pipelineBuilderCollection = new Collection<IPipelineBuilder>();
            _asyncPipelineBuilderCollection = new Collection<IPipelineAsyncBuilder>();
            _cancellablePipelineBuilderCollection = new Collection<ICancellablePipelineAsyncBuilder>();
        }

        /// <summary>
        /// Returns a PipelineBehavior for a Message Type
        /// </summary>
        /// <typeparam name="TRequest">MessageType</typeparam>
        /// <returns>PipelineBehavior</returns>
        public IPipelineBehavior<TRequest> On<TRequest>()
        {
            var behavior = new PipelineBehavior<TRequest>(this);
            return behavior;
        }

        /// <summary>
        /// Adds a Pipeline
        /// </summary>
        /// <param name="pipelineBuilder">PipelineBuilder</param>
        /// <returns>PipelineBuilder</returns>
        public IPipelineBuilder Add(IPipelineBuilder pipelineBuilder)
        {
            _pipelineBuilderCollection.Add(pipelineBuilder);
            return pipelineBuilder;
        }

        /// <summary>
        /// Adds a Pipeline
        /// </summary>
        /// <param name="pipelineBuilder">PipelineBuilder</param>
        /// <returns>PipelineBuilder</returns>
        public IPipelineAsyncBuilder Add(IPipelineAsyncBuilder pipelineBuilder)
        {
            _asyncPipelineBuilderCollection.Add(pipelineBuilder);
            return pipelineBuilder;
        }

        /// <summary>
        /// Adds a Pipeline
        /// </summary>
        /// <param name="pipelineBuilder">PipelineBuilder</param>
        /// <returns>PipelineBuilder</returns>
        public ICancellablePipelineAsyncBuilder Add(ICancellablePipelineAsyncBuilder pipelineBuilder)
        {
            _cancellablePipelineBuilderCollection.Add(pipelineBuilder);
            return pipelineBuilder;
        }

        /// <summary>
        /// Builds a PipelineProvider
        /// </summary>
        /// <returns>Returns a immutable PipelineProvider</returns>
        public IPipelineProvider Build()
        {
            var pipelineCollection = new PipelineCollection<IPipeline>();
            var asyncPipelineCollection = new PipelineCollection<IPipelineAsync>();
            var cancellablePipelineCollection = new PipelineCollection<ICancellablePipelineAsync>();

            foreach (var item in _pipelineBuilderCollection)
            {
                var pipeline = item.Build();
                pipelineCollection.Add(pipeline);
            }

            foreach (var item in _asyncPipelineBuilderCollection)
            {
                var pipeline = item.Build();
                asyncPipelineCollection.Add(pipeline);
            }

            foreach (var item in _cancellablePipelineBuilderCollection)
            {
                var pipeline = item.Build();
                cancellablePipelineCollection.Add(pipeline);
            }

            return new PipelineProvider(
                pipelineCollection,
                asyncPipelineCollection,
                cancellablePipelineCollection
            );
        }
    }
}