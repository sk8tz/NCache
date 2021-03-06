// Copyright (c) 2015 Alachisoft
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//    http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
using System;
using System.Collections;

namespace Alachisoft.NCache.Caching.Queries.Filters
{
    public class FunctorLesserFunctorPredicate : Predicate, IComparable
    {
        IFunctor functor;
        IFunctor generator;

        public FunctorLesserFunctorPredicate(IFunctor lhs, IFunctor rhs)
        {
            functor = lhs;
            generator = rhs;
        }

        public override bool ApplyPredicate(object o)
        {
            object lhs = functor.Evaluate(o);
            object rhs = generator.Evaluate(o);
            return Comparer.Default.Compare(lhs, rhs) < 0;
        }

        public override string ToString()
        {
            return functor + (Inverse ? " >= " : " < ") + generator;
        }
        #region IComparable Members

        public int CompareTo(object obj)
        {
            if (obj is FunctorLesserFunctorPredicate)
            {
                FunctorLesserFunctorPredicate other = (FunctorLesserFunctorPredicate)obj;
                if (Inverse == other.Inverse)
                    return ((IComparable)functor).CompareTo(other.functor) == 0 &&
                           ((IComparable)generator).CompareTo(other.generator) == 0 ? 0 : -1;
            }
            return -1;
        }

        #endregion
    }
}
