﻿using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using ScriptEngine.HostedScript.Library;
using ScriptEngine.Machine;
using ScriptEngine.Machine.Contexts;

namespace OneScript.WebHost.Application
{
    [ContextClass("КоллекцияФайловФормы")]
    public class FormFilesCollectionContext : AutoContext<FormFilesCollectionContext>, ICollectionContext, IEnumerable<FormFileContext>
    {
        private readonly List<FormFileContext> _data = new List<FormFileContext>();
        private readonly Dictionary<string, int> _indexer = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

        public FormFilesCollectionContext(IFormFileCollection data)
        {
            int i = 0;
            foreach (var fFile in data)
            {
                _data.Add(new FormFileContext(fFile));
                _indexer[fFile.Name] = i++;
            }
        }

        public override bool IsIndexed => true;

        public override IValue GetIndexedValue(IValue index)
        {
            if (index.DataType == DataType.Number)
                return this[(int) index.AsNumber()];

            if (index.DataType == DataType.String)
                return this[index.AsString()];

            throw RuntimeException.InvalidArgumentType(nameof(index));
        }

        public FormFileContext this[int index] => _data[index];

        public FormFileContext this[string index]
        {
            get
            {
                var intIdx = _indexer[index];
                return _data[intIdx];
            }
        }

        public int Count()
        {
            return _data.Count;
        }

        public CollectionEnumerator GetManagedIterator()
        {
            return new CollectionEnumerator(GetEnumerator());
        }

        public IEnumerator<FormFileContext> GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}