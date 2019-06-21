namespace EDDMobileImpl.Services
{
    public class PropertyCopier<TSource, TTarget> where TSource : struct
                                            where TTarget : class
    {
        public static void Copy(TSource source, TTarget target)
        {
            var sourceFields = source.GetType().GetFields();
            var targetProps = target.GetType().GetProperties();

            foreach (var sourceField in sourceFields)
            {
                foreach (var targetProp in targetProps)
                {
                    if (sourceField.Name.EqualsAlphaNumOnlyNoCase(targetProp.Name) && sourceField.FieldType == targetProp.PropertyType)
                    {
                        targetProp.SetValue(target, sourceField.GetValue(source));
                        break;
                    }
                }
            }
        }
    }
}