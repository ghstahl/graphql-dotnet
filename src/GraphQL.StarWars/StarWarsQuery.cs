using GraphQL.StarWars.Types;
using GraphQL.Types;

namespace GraphQL.StarWars
{
    public class StarWarsQuery : ObjectGraphType<object>
    {
        public StarWarsQuery(StarWarsData data)
        {
            Name = "Query";

            Field<CharacterInterface>("hero", resolve: context => data.GetDroidByIdAsync("3"));
            Field<HumanType>(
                "human",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> {Name = "id", Description = "id of the human"}
                    ),
                resolve: context => data.GetHumanByIdAsync(context.GetArgument<string>("id"))
                );
            Field<DroidType>(
                "droid",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> {Name = "id", Description = "id of the droid"}
                    ),
                resolve: context => data.GetDroidByIdAsync(context.GetArgument<string>("id"))
                );
        }
    }

    public class StarWarsMutation : ObjectGraphType<object>
    {
        public StarWarsMutation(StarWarsData data)
        {
            Name = "Query";


            Field<HumanType>(
                "human",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> {Name = "id", Description = "id of the human"},
                    new QueryArgument<NonNullGraphType<StringGraphType>>
                    {
                        Name = "name",
                        Description = "name of the human"
                    }

                    ),
                resolve: context =>
                {
                    var id = context.GetArgument<string>("id");
                    var name = context.GetArgument<string>("name");
                    var result = data.UpdateHumanByIdAsync(id, name);
                    return result;
                }
                );

        }
    }
}
